using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "Stargate API";
    config.Version = "v1";
});

builder.Services.AddDbContext<StargateContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("StargateDB")));

// Register MediatR and pipeline behaviors
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(Program).Assembly,                     // Stargate.API
        typeof(CreatePersonCommand).Assembly          // Stargate.Application
    );
});

// Register MediatR PreProcessor support
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
builder.Services.AddTransient<IRequestPreProcessor<CreatePersonCommand>, CreatePersonPreProcessor>();

// Register AutoMappers
builder.Services.AddAutoMapper(typeof(PersonProfile).Assembly);
builder.Services.AddAutoMapper(typeof(AstronautDutyProfile).Assembly);

// Register repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IAstronautDutyRepository, AstronautDutyRepository>();

var app = builder.Build();

//// Configure the HTTP request pipeline
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();      // Serves /swagger/v1/swagger.json
    app.UseSwaggerUi();   // Serves Swagger UI at /swagger
}

// Use custom global exception handler middleware
app.UseCustomExceptionHandler();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
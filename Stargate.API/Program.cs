var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Stargate API",
        Version = "v1"
    });

    c.AddServer(new OpenApiServer
    {
        Url = "/stargate-api"
    });
});

builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "Stargate API";
    config.Version = "v1";
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// SQL connection string using sa
var saPassword = Environment.GetEnvironmentVariable("SA_PASSWORD");
var connectionString = $"Server=192.168.50.223;Database=StargateDB;User Id=sa;Password={saPassword};Encrypt=False;";

// DbContext
builder.Services.AddDbContext<StargateContext>(options =>
    options.UseSqlServer(connectionString));

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

app.UsePathBase("/stargate-api");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/stargate-api/swagger/v1/swagger.json", "Stargate API V1");
        c.RoutePrefix = "swagger";
    });

    app.UseOpenApi();
    app.UseSwaggerUi();
}

// Use custom global exception handler middleware
app.UseCustomExceptionHandler();

//app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
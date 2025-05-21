using Azure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using System;

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

bool.TryParse(builder.Configuration["LOCAL_DEVELOPMENT"], out bool isLocalDev);
var sqlServer = builder.Configuration["Database:Server"];
var database = builder.Configuration["Database:Name"];

// DbContext
builder.Services.AddDbContext<StargateContext>(options =>
{
    SqlConnection connection;
    if (isLocalDev)
    {
        var connectionString = $"Data Source={sqlServer};Initial Catalog={database};User ID=wch94_aol.com#EXT#@wch94aol.onmicrosoft.com;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Authentication=ActiveDirectoryInteractive;Application Intent=ReadWrite;Multi Subnet Failover=False";
        connection = new SqlConnection(connectionString);
    }
    else
    {
        var connectionString = $"Server={sqlServer};Database={database};";
        var credential = new DefaultAzureCredential();
        var token = credential.GetToken(new Azure.Core.TokenRequestContext(new[] { "https://database.windows.net/" }));
        connection = new SqlConnection(connectionString) { AccessToken = token.Token };
    }
    options.UseSqlServer(connection);
});

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
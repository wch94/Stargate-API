var builder = WebApplication.CreateBuilder(args);

var rawConnStr = builder.Configuration.GetConnectionString("StargateDb");
var saPassword = Environment.GetEnvironmentVariable("SA_PASSWORD");

if (string.IsNullOrWhiteSpace(saPassword))
{
    throw new InvalidOperationException("SA_PASSWORD environment variable is not set.");
}

var connectionString = rawConnStr.Replace("{SA_PASSWORD}", saPassword);

var sinkOpts = new MSSqlServerSinkOptions
{
    TableName = "Logs",
    AutoCreateSqlTable = true
};

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: sinkOpts,
        restrictedToMinimumLevel: LogEventLevel.Warning
    )
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
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

// Register FluentValidations
builder.Services.AddValidatorsFromAssembly(typeof(CreatePersonValidator).Assembly);
builder.Services.AddFluentValidationAutoValidation();

// Register ValidationBehavior for FluentValidation
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

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
namespace Stargate.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        int statusCode = (int)HttpStatusCode.InternalServerError;
        string message = "An unexpected error occurred.";

        if (exception is BaseResponseException baseEx)
        {
            statusCode = baseEx.ResponseCode;
            message = baseEx.Message;
        }

        context.Response.StatusCode = statusCode;

        var result = JsonSerializer.Serialize(new BaseResponse<object>
        {
            Success = false,
            Message = message,
            ResponseCode = statusCode,
            Data = null
        });

        await context.Response.WriteAsync(result);
    }
}
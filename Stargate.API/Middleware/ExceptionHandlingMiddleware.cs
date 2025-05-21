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

        int statusCode = exception switch
        {
            ConflictException => (int)HttpStatusCode.Conflict,
            NotFoundException => (int)HttpStatusCode.NotFound,
            ValidationException => (int)HttpStatusCode.BadRequest,
            CustomValidationException => (int)HttpStatusCode.BadRequest,
            BaseResponseException bre => bre.ResponseCode,
            _ => (int)HttpStatusCode.InternalServerError
        };

        object response;

        if (exception is ValidationException fvEx)
        {
            response = new
            {
                Success = false,
                Message = "Validation failed",
                ResponseCode = statusCode,
                Errors = fvEx.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    )
            };
        }
        else if (exception is CustomValidationException cve)
        {
            response = new
            {
                Success = false,
                Message = exception.Message,
                ResponseCode = (int)HttpStatusCode.BadRequest,
                Errors = cve.Errors
            };
        }
        else
        {
            response = new BaseResponse
            {
                Success = false,
                Message = exception.Message,
                ResponseCode = statusCode
            };
        }

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
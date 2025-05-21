namespace Stargate.Application.Common.Exceptions;

public class CustomValidationException : BaseResponseException
{
    public IDictionary<string, string[]> Errors { get; }

    public CustomValidationException(string field, string message)
        : base("Validation failed")
    {
        Errors = new Dictionary<string, string[]>
        {
            { field, new[] { message } }
        };
    }
}

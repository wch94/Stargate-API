namespace Stargate.Application.Common.Exceptions;

public class BaseResponseException : Exception
{
    public int ResponseCode { get; }
    public bool Success { get; } = false;

    public BaseResponseException(string message, int responseCode = (int)HttpStatusCode.BadRequest)
        : base(message)
    {
        ResponseCode = responseCode;
    }
}
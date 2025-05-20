namespace Stargate.Application.Common.Exceptions;

public class ConflictException : BaseResponseException
{
    public ConflictException(string message)
        : base(message, (int)HttpStatusCode.Conflict) { }
}
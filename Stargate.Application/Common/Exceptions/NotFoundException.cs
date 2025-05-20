namespace Stargate.Application.Common.Exceptions;

public class NotFoundException : BaseResponseException
{
    public NotFoundException(string message)
        : base(message, (int)HttpStatusCode.NotFound) { }
}
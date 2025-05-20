namespace Stargate.Application.Common.Responses;

public class BaseResponse<T>
{
    public BaseResponse() { }
    public BaseResponse(T data) => Data = data;
    public bool Success { get; set; } = true;
    public string Message { get; set; } = "Successful";
    public int ResponseCode { get; set; } = (int)HttpStatusCode.OK;
    public T? Data { get; set; }
}
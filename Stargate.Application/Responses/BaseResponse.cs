﻿namespace Stargate.Application.Responses;

public class BaseResponse
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = "Successful";
    public int ResponseCode { get; set; } = (int)HttpStatusCode.OK;
}
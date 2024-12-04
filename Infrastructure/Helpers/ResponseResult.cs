namespace Infrastructure.Helpers;


public enum StatusCode
{
    OK = 200,
    CREATED = 201,
    BAD_REQUEST = 400,
    NOT_FOUND = 404,
    EXISTS = 409
}

public class ResponseResult
{
    public StatusCode StatusCode { get; set; }
    public object? Content { get; set; }
    public string? Message { get; set; }
}

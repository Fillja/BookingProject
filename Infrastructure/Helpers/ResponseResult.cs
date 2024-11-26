namespace Infrastructure.Helpers;


public enum StatusCodes
{
    OK = 200,
    CREATED = 201,
    BAD_REQUEST = 400,
    NOT_FOUND = 404,
    CONFLICT = 409
}

public class ResponseResult
{
    public StatusCodes StatusCode { get; set; }
    public object? Content { get; set; }
    public string? Message { get; set; }
}

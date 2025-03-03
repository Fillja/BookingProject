namespace Infrastructure.Helpers;


//public enum StatusCode
//{
//    OK = 0,
//    FAILED = 1,
//    NOT_FOUND = 2,
//    CONFLICT = 3
//}

public class ResponseResult
{
    public int StatusCode { get; set; }
    public object? Content { get; set; }
    public string? Message { get; set; }
    public bool HasFailed { get; set; }

    public static ResponseResult Result(int statusCode, string message = null!, object obj = null!)
    {
        return new ResponseResult
        {
            Content = obj ?? "",
            Message = message ?? "Succeeded.",
            StatusCode = statusCode,
            HasFailed = statusCode > 0
        };
    }
}


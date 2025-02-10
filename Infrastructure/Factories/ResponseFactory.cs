using Infrastructure.Helpers;

namespace Infrastructure.Factories;

public class ResponseFactory
{
    public static ResponseResult Ok(string message = null!)
    {
        return new ResponseResult
        {
            Message = message ?? "Succeeded.",
            StatusCode = StatusCode.OK
        };
    }

    public static ResponseResult Ok(object obj, string message = null!)
    {
        return new ResponseResult
        {
            Content = obj,
            Message = message ?? "Succeeded.",
            StatusCode = StatusCode.OK,
        };
    }

    public static ResponseResult BadRequest(string message = null!)
    {
        return new ResponseResult
        {
            Message = message ?? "Failed.",
            StatusCode = StatusCode.BAD_REQUEST,
        };
    }

    public static ResponseResult NotFound(string message = null!)
    {
        return new ResponseResult
        {
            Message = message ?? "Not found.",
            StatusCode = StatusCode.NOT_FOUND,
        };
    }

    public static ResponseResult Exists(string message = null!)
    {
        return new ResponseResult
        {
            Message = message ?? "Exists.",
            StatusCode = StatusCode.EXISTS,
        };
    }
}

namespace Infrastructure.Helpers;

public static class HttpErrorHandler
{
    public static bool HasHttpError(ResponseResult responseResult)
    {
        if (responseResult.StatusCode == StatusCode.NOT_FOUND || responseResult.StatusCode == StatusCode.BAD_REQUEST)
            return true;

        return false;
    }
}

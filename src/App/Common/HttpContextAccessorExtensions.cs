namespace Smeuj.Platform.App.Common;

public static class HttpContextAccessorExtensions {
    public static bool IsHtmx(this IHttpContextAccessor accessor) {
        var request = accessor.HttpContext?.Request;
        if (request == null) {
            return false;
        }

        return request.Headers["HX-Request"] == "true";
    }
}
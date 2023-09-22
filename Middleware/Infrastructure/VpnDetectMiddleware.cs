namespace Middleware.Infrastructure;
public class VpnDetectMiddleware
{
    private readonly RequestDelegate _next;
    public VpnDetectMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var countryCode = "::1";

        var ipAddress = context.Connection.RemoteIpAddress.ToString();

        // .... countryCode = api call get countryCode (ipAddress)

        if (countryCode != ipAddress)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Access denied.....");
            //return;
            await _next(context);
        }

        await _next(context);
    }
}
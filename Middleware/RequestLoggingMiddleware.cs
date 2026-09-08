namespace UserManagementAPI.Middleware
{
public class RequestLoggingMiddleware
{
private readonly RequestDelegate _next;
private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        DateTime startTime = DateTime.UtcNow;

        _logger.LogInformation(
            "Incoming request: {Method} {Path}",
            context.Request.Method,
            context.Request.Path
        );

        await _next(context);

        TimeSpan duration = DateTime.UtcNow - startTime;

        _logger.LogInformation(
            "Outgoing response: {StatusCode} for {Method} {Path} in {Duration}ms",
            context.Response.StatusCode,
            context.Request.Method,
            context.Request.Path,
            duration.TotalMilliseconds
        );
    }
}
}

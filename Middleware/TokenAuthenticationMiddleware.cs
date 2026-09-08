namespace UserManagementAPI.Middleware
{
public class TokenAuthenticationMiddleware
{
private readonly RequestDelegate _next;
    private const string ValidToken = "techhive-secret-token";

    public TokenAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }

        if (context.Request.Method == "GET" &&
            context.Request.Path == "/")
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
        {
            await UnauthorizedResponse(context);
            return;
        }

        string authorization = authorizationHeader.ToString();

        if (!authorization.StartsWith("Bearer "))
        {
            await UnauthorizedResponse(context);
            return;
        }

        string token = authorization.Substring("Bearer ".Length).Trim();

        if (token != ValidToken)
        {
            await UnauthorizedResponse(context);
            return;
        }

        await _next(context);
    }

    private static async Task UnauthorizedResponse(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            error = "Unauthorized. A valid token is required."
        });
    }
}
}

namespace GloboClimaPlatform.Infra.Middlewares;

public class JwtCookieMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Cookies.ContainsKey("JwtToken"))
        {
            var token = context.Request.Cookies["JwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers.Append("Authorization", "Bearer " + token);
            }
        }

        await _next(context);
    }
}
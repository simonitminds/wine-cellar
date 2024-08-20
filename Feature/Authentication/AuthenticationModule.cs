using System.Security.Claims;
using Carter;
using Carter.OpenApi;

namespace WineCellar.Feature.Authentication;

public abstract class LoginRequest
{
    public string Username { get; set; } = string.Empty;
}

public class AuthenticationModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/login",
                (LoginRequest pa, HttpContext _, HttpResponse _) => LoginQuery.Execute(pa.Username))
            .Produces<AuthResponse>()
            .WithTags("Authentication")
            .WithName("Login")
            .IncludeInOpenApi();

        app.MapGet(
                "/protected",
                (HttpContext context, HttpResponse response) =>
                {
                    var user = context.User;

                    if (user.Identity?.IsAuthenticated ?? false)
                    {
                        response.StatusCode = 200;
                        response.ContentType = "text/plain";
                        return
                            $"User is Authenticated with the following username: \"{user.FindFirst(ClaimTypes.Name)?.Value}\"";
                    }

                    return "Hello World";
                }
            )
            .RequireAuthorization()
            .WithTags("Authentication")
            .WithName("Protected")
            .IncludeInOpenApi();
    }
}
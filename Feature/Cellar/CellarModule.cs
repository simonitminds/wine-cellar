using Carter;
using Carter.OpenApi;
using WineCellar.Persistence;

namespace WineCellar.Feature.Cellar;

public class CellarModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/cellar", (HttpContext context) => "Hello Cellar")
            .WithTags("Cellar")
            .WithName("Cellar")
            .RequireAuthorization()
            .IncludeInOpenApi();

        app.MapGet("/cellar/{id:int}", (int id) => $"Hello Cellar {id}")
            .WithTags("Cellar")
            .WithName("Cellar by id")
            .RequireAuthorization()
            .IncludeInOpenApi();

        app.MapPost(
                "/cellar",
                (
                    CreateCellar.CreateCellarInput input,
                    ApplicationDbContext context,
                    CancellationToken token
                ) => new CreateCellar().ExecuteAsync(input, context, token)
            )
            .WithTags("Cellar")
            .WithName("Create Cellar")
            .RequireAuthorization()
            .IncludeInOpenApi();
    }
}

using System.Security.Claims;
using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WineCellar.Domain;
using WineCellar.Persistence;

namespace WineCellar.Feature.Wines;

public class WinesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/wines",
                (HttpContext context, ApplicationDbContext dbContext) =>
                {
                    var name = context.User?.Identity?.Name;
                    if (name is null)
                    {
                        throw new UnauthorizedAccessException();
                    }

                    var wines = dbContext.Users.First(x => x.Username == name).Wines;

                    return wines;
                }
            )
            .Produces<List<Wine>>()
            .RequireAuthorization()
            .WithTags("Wines")
            .WithName("GetWines")
            .IncludeInOpenApi();
    }
}

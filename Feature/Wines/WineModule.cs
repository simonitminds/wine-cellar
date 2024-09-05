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
                    var user = context.User;
                    if (user is null)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    var name = context.User.Identity?.Name;
                    var wines = dbContext.Users.FirstOrDefault(x => x.Username == name)?.Wines;

                    return wines;
                }
            )
            .Produces<List<Wine>>()
            .WithTags("Wines")
            .WithName("GetWines")
            .IncludeInOpenApi();
    }
}

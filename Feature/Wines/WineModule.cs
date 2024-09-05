using System.Security.Claims;
using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WineCellar.Domain;
using WineCellar.Persistence;

namespace WineCellar.Feature.Wines;

public class WineRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Type { get; set; } = string.Empty;
    public int Quantity { get; set; }
}

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

                    var wines = dbContext
                        .Users.Include(x => x.Wines)
                        .First(x => x.Username == name)
                        .Wines;

                    return wines;
                }
            )
            .Produces<List<Wine>>()
            .RequireAuthorization()
            .WithTags("Wines")
            .WithName("GetWines")
            .IncludeInOpenApi();

        app.MapPost(
                "/wines/add",
                (HttpContext context, WineRequest wine, ApplicationDbContext dbContext) =>
                {
                    var name = context.User.Identity?.Name;
                    if (name is null)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    var newWine = new Wine
                    {
                        Name = wine.Name,
                        Year = wine.Year,
                        Type = wine.Type,
                        Quantity = wine.Quantity,
                    };
                    dbContext.Users.First(x => x.Username == name).Wines.Add(newWine);
                    dbContext.SaveChanges();

                    return newWine;
                }
            )
            .WithTags("Wines")
            .WithName("AddWine")
            .IncludeInOpenApi()
            .RequireAuthorization();
    }
}

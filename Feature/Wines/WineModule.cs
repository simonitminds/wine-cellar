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
                "/wines/",
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

        app.MapGet(
                "/wine/",
                (HttpContext context, ApplicationDbContext dbContext, [FromBody] Wine userWine) =>
                {
                    var user = context.User;
                    if (user is null)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    var name = context.User.Identity?.Name;
                    var wine = dbContext
                        .Users.FirstOrDefault(x => x.Username == name)
                        ?.Wines.Find(y => y.Id == userWine.Id);

                    return wine;
                }
            )
            .Produces<Wine>()
            .WithTags("Wines")
            .WithName("GetWine")
            .IncludeInOpenApi();

        app.MapGet(
                "/winesByName/",
                (HttpContext context, ApplicationDbContext dbContext, string userWines) =>
                {
                    var user = context.User;
                    if (user is null)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    var name = context.User.Identity?.Name;
                    var wines = dbContext
                        .Users.FirstOrDefault(x => x.Username == name)
                        ?.Wines.Where(y => y.Name.Contains(userWines));

                    return wines;
                }
            )
            .Produces<List<Wine>>()
            .WithTags("Wines")
            .WithName("GetWineByName")
            .IncludeInOpenApi();

        app.MapDelete(
                "/delete/",
                (HttpContext context, ApplicationDbContext dbContext, [FromBody] Wine userWine) =>
                {
                    var existingWine = dbContext.Wines.Find(userWine.Id);
                    if (existingWine is null)
                    {
                        return Results.NotFound("Wine not found");
                    }
                    dbContext.Remove(userWine);
                    dbContext.SaveChanges();

                    return Results.Ok("Wine deleted successfully.");
                }
            )
            .RequireAuthorization()
            .WithTags("Wines")
            .WithName("DeleteWine")
            .IncludeInOpenApi();

        app.MapPost(
                "/update/",
                (HttpContext context, ApplicationDbContext dbContext, [FromBody] Wine userWine) =>
                {
                    var existingWine = dbContext.Wines.Find(userWine.Id);
                    if (existingWine is null)
                    {
                        return Results.NotFound("Wine not found");
                    }
                    dbContext.Update(userWine);
                    dbContext.SaveChanges();

                    return Results.Ok("Wine updated successfully.");
                }
            )
            .RequireAuthorization()
            .WithTags("Wines")
            .WithName("UpdateWine")
            .IncludeInOpenApi();
    }
}

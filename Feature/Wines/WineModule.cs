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

        app.MapGet(
                "/winesByName",
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
                "/delete",
                (HttpContext context, ApplicationDbContext dbContext, int wineId) =>
                {
                    var existingWine = dbContext.Wines.Find(wineId);
                    if (existingWine is null)
                    {
                        return Results.NotFound("Wine not found");
                    }
                    dbContext.Remove(existingWine);
                    dbContext.SaveChanges();

                    return Results.Ok("Wine deleted successfully.");
                }
            )
            .RequireAuthorization()
            .WithTags("Wines")
            .WithName("DeleteWine")
            .IncludeInOpenApi();

        app.MapPost(
                "/update",
                (HttpContext context, ApplicationDbContext dbContext, WineRequest userWine) =>
                {
                    var existingWine = dbContext.Wines.FirstOrDefault(wine =>
                        wine.Id == userWine.Id
                    );
                    if (existingWine is null)
                    {
                        return Results.NotFound("Wine not found");
                    }
                    existingWine.Name = userWine.Name;
                    existingWine.Quantity = userWine.Quantity;
                    existingWine.Type = userWine.Type;
                    existingWine.Year = userWine.Year;
                    dbContext.Update(existingWine);
                    dbContext.SaveChanges();

                    return Results.Ok("Wine updated successfully.");
                }
            )
            .RequireAuthorization()
            .WithTags("Wines")
            .WithName("UpdateWine")
            .IncludeInOpenApi();

        app.MapGet(
                "/wine",
                (HttpContext context, ApplicationDbContext dbContext, int wineId) =>
                {
                    var userIdString = context
                        .User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier)
                        .Value;
                    var userId = int.Parse(userIdString);
                    var wine = dbContext
                        .Wines.Where(wine => wine.UserId == userId)
                        .FirstOrDefault(x => x.Id == wineId);
                    return wine;
                }
            )
            .Produces<Wine>()
            .WithTags("Wines")
            .WithName("GetWine")
            .IncludeInOpenApi();
    }
}

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
    public string Description { get; set; } = string.Empty;
    public int? ExpirationDate { get; set; }
    public int StorageId { get; set; }
}

public class WinesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/wines",
                (HttpContext context, ApplicationDbContext dbContext, int CellarId) =>
                {
                    var name = context.User?.Identity?.Name;
                    if (name is null)
                    {
                        throw new UnauthorizedAccessException();
                    }

                    var wines = dbContext
                        .Cellars.First(x => x.Id == CellarId)
                        .Storages.SelectMany(storage => storage.Wines)
                        .ToList();

                    return wines;
                }
            )
            .Produces<List<Wine>>()
            .RequireAuthorization()
            .WithTags("Wines")
            .WithName("GetWines")
            .IncludeInOpenApi();

        // app.MapPost(
        //         "/wines/add",
        //         (HttpContext context, WineRequest wine, ApplicationDbContext dbContext) =>
        //         {
        //             var newWine = new Wine(wine);
        //             var userId = context.GetUserId();
        //             dbContext.Users.First(x => x.Id == userId).Wines.Add(newWine);
        //             dbContext.SaveChanges();
        //             return newWine;
        //         }
        //     )
        //     .WithTags("Wines")
        //     .WithName("AddWine")
        //     .IncludeInOpenApi()
        //     .RequireAuthorization();

        app.MapDelete(
                "/delete/{wineId:int}",
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

                    WineMutator.Mutatewine(userWine, existingWine);
                    dbContext.SaveChanges();

                    return Results.Ok(existingWine);
                }
            )
            .RequireAuthorization()
            .WithTags("Wines")
            .WithName("UpdateWine")
            .IncludeInOpenApi();

        // app.MapGet(
        //         "/wine/{wineId:int}",
        //         (HttpContext context, ApplicationDbContext dbContext, int wineId) =>
        //         {
        //             var wine = dbContext
        //                 .Wines.Where(wine => wine.UserId == context.GetUserId())
        //                 .FirstOrDefault(x => x.Id == wineId);
        //             return wine;
        //         }
        //     )
        //     .Produces<Wine>()
        //     .WithTags("Wines")
        //     .WithName("GetWine")
        //     .IncludeInOpenApi();
    }
}

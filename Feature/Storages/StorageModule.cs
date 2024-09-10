using System.Security.Claims;
using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WineCellar.Domain;
using WineCellar.Persistence;

namespace WineCellar.Feature.Storages;

public class StorageRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double Temperature { get; set; }
    public uint Capacity { get; set; }
}

public class StorageModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/storage/add",
                (HttpContext context, StorageRequest storage, ApplicationDbContext dbContext) =>
                {
                    var newStorage = new Storage(storage);
                    var userId = context.GetUserId();
                    dbContext.Users.First(x => x.Id == userId).Storage.Add(newStorage);
                    dbContext.SaveChanges();

                    return newStorage;
                }
            )
            .WithTags("Storage")
            .WithName("AddStorage")
            .IncludeInOpenApi()
            .RequireAuthorization();

        app.MapGet(
                "/storages",
                (HttpContext context, ApplicationDbContext dbContext) =>
                {
                    var storage = dbContext.Storages.Where(storage =>
                        storage.UserId == context.GetUserId()
                    );

                    return storage;
                }
            )
            .Produces<List<Storage>>()
            .RequireAuthorization()
            .WithTags("Storage")
            .WithName("GetStorages")
            .IncludeInOpenApi();

        app.MapDelete(
                "/storage/delete",
                (HttpContext context, ApplicationDbContext dbContext, int storageId) =>
                {
                    var existingStorage = dbContext.Storages.Find(storageId);
                    if (existingStorage is null)
                    {
                        return Results.NotFound("Storage not found");
                    }
                    dbContext.Remove(existingStorage);
                    dbContext.SaveChanges();
                    return Results.Ok("Storage deleted successfully.");
                }
            )
            .Produces<OkResult>()
            .RequireAuthorization()
            .WithTags("Storage")
            .WithName("DeleteStorage")
            .IncludeInOpenApi();

        app.MapPost(
                "/storage/update",
                (HttpContext context, ApplicationDbContext dbContext, StorageRequest userStorage) =>
                {
                    var existingStorage = dbContext.Storages.FirstOrDefault(StorageModule =>
                        StorageModule.Id == userStorage.Id
                    );
                    if (existingStorage is null)
                    {
                        return Results.NotFound("Storage not found");
                    }
                    StorageMutator.MutateStorage(userStorage, existingStorage);

                    dbContext.SaveChanges();

                    return Results.Ok(existingStorage);
                }
            )
            .Produces<Storage>()
            .RequireAuthorization()
            .WithTags("Storage")
            .WithName("UpdateStorage")
            .IncludeInOpenApi();

        app.MapGet(
                "storage/{storageId:int}",
                (HttpContext context, ApplicationDbContext dbContext, int storageId) =>
                {
                    var storage = dbContext
                        .Storages.Where(storage => storage.UserId == context.GetUserId())
                        .FirstOrDefault(x => x.Id == storageId);
                    return storage;
                }
            )
            .Produces<Storage>()
            .RequireAuthorization()
            .WithTags("Storage")
            .WithName("GetStorage")
            .IncludeInOpenApi();
    }
}

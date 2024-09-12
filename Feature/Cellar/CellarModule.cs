using System;
using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WineCellar.Domain;
using WineCellar.Persistence;

namespace WineCellar.Feature.Cellars
{
    public class CellarRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public double Temperature { get; set; }
    }

    public class CellarModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                    "/cellars",
                    (HttpContext context, ApplicationDbContext dbContext) =>
                    {
                        var cellars = dbContext
                            .Cellars.Where(cellar =>
                                cellar.Users.FirstOrDefault(user => user.Id == context.GetUserId())
                                != null
                            )
                            .ToList();
                        return cellars;
                    }
                )
                .Produces<List<Cellar>>()
                .RequireAuthorization()
                .WithTags("Cellar")
                .WithName("GetCellars")
                .IncludeInOpenApi();
        }
    }
}

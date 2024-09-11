using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WineCellar.Feature.Wines;

namespace WineCellar.Domain;

public class Wine
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Type { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Description { get; set; } = string.Empty;
    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public int? StorageId { get; set; }

    public Storage? Storage { get; set; }

    public Wine(WineRequest request)
    {
        Name = request.Name;
        Year = request.Year;
        Type = request.Type;
        Quantity = request.Quantity;
        Description = request.Description;
        StorageId = request.StorageId;
    }

    public Wine() { }
}

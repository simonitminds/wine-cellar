using WineCellar.Feature.Storages;

namespace WineCellar.Domain;

public class Storage
{
    public string Name { get; set; } = string.Empty;
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public double Temperature { get; set; }
    public uint Capacity { get; set; }
    public int CellarId { get; set; }
    public Cellar Cellar { get; set; } = null!;
    public List<Wine> Wines { get; set; } = new();

    public Storage(StorageRequest request)
    {
        Name = request.Name;
        Type = request.Type;
        Temperature = request.Temperature;
        Capacity = request.Capacity;
        CellarId = request.CellarId;
    }

    public Storage() { }
}

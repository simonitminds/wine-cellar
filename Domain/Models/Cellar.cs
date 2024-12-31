using WineCellar.Feature.Cellars;

namespace WineCellar.Domain;

public class Cellar
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Temperature { get; set; } = 15.0;
    public string Location { get; set; } = string.Empty;
    public List<Storage> Storages { get; set; } = new();
    public List<User> Users { get; set; } = new();

    public Cellar(CellarRequest request)
    {
        Name = request.Name;
        Temperature = request.Temperature;
        Location = request.Location;
    }

    public Cellar() { }
}

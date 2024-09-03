namespace WineCellar.Domain;

public class Wine
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Year { get; set; }

    //public List<User> Users { get; set; } = new();
    
}
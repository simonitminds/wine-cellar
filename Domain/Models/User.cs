namespace WineCellar.Domain;

public class User
{
    public string Username { get; set; } = string.Empty;
    public int Id { get; set; }
    public List<Cellar> Cellars { get; set; } = new();
}

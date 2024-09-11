namespace WineCellar.Domain;

public class User
{
    public string Username { get; set; } = string.Empty;
    public int Id { get; set; }
    public List<Wine> Wines { get; set; } = new();

    public List<Storage> Storage { get; set; } = new();
}

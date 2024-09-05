using System.ComponentModel.DataAnnotations;

namespace WineCellar.Domain;

public class Wine
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Type { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}

namespace WineCellar.Feature.Authentication;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public int UserId { get; set; }
}

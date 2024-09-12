using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using WineCellar.Auth;
using WineCellar.Domain;
using WineCellar.Persistence;

namespace WineCellar.Feature.Authentication;

public static class LoginQuery
{
    public static AuthResponse Execute(string username)
    {
        using var db = new ApplicationDbContext();

        var user = db.Users.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            user = new User { Username = username };
            db.Users.Add(user);
            db.SaveChanges();
        }

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(AuthConstants.SigningKey)
        );
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Console.WriteLine(user);
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new("UserData", JsonSerializer.Serialize(user)),
        };

        var token = new JwtSecurityToken(
            AuthConstants.Issuer,
            AuthConstants.Audience,
            claims,
            expires: DateTime.Now.AddHours(4),
            signingCredentials: signingCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new AuthResponse
        {
            Token = tokenString,
            Username = username,
            UserId = user.Id,
        };
    }
}

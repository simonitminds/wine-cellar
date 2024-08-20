using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using JwtSample.Auth;
using Microsoft.IdentityModel.Tokens;

public class LoginQuery
{
    public AuthResponse Execute(string username)
    {
        using var db = new ApplicationDbContext();

        var user = db.Users.Where(u => u.Username == username).FirstOrDefault();
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

        System.Console.WriteLine(user);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("UserData", JsonSerializer.Serialize(user)),
        };

        var token = new JwtSecurityToken(
            issuer: AuthConstants.Issuer,
            audience: AuthConstants.Audience,
            claims: claims,
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

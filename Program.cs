using JwtSample.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = AuthConstants.Issuer,
        ValidAudience = AuthConstants.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConstants.SigningKey)),
        ClockSkew = TimeSpan.FromMinutes(5)
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/token", async context =>
{
    if (!context.Request.Query.TryGetValue("username", out var userName))
    {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("You must supply a username in the query params", context.RequestAborted);

        return;
    }

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConstants.SigningKey));
    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new List<Claim>()
    {
        new Claim(ClaimTypes.NameIdentifier, userName),
        //TODO: Add claims to the token here
    };

    var token = new JwtSecurityToken(issuer: AuthConstants.Issuer, audience: AuthConstants.Audience, claims: claims,
        expires: DateTime.Now.AddHours(4), signingCredentials: signingCredentials);
    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

    context.Response.StatusCode = 200;
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsJsonAsync(new { token = tokenString });
});
app.MapGet("/authorized", async context =>
{
    var user = context.User;

    if (user.Identity?.IsAuthenticated ?? false)
    {
        context.Response.StatusCode = 200;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync($"User is Authenticated with the following username: \"{user.FindFirst(ClaimTypes.NameIdentifier).Value}\"", context.RequestAborted);
    }
}).RequireAuthorization();

app.Run();

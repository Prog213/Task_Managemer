using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services;

public class TokenProvider(IConfiguration config) : ITokenProvider
{
    public string CreateToken(User user)
    {
        // Getting token key from appsettings.json
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appsettings");
        // Checking if tokenKey is long enough to match the security requirements
        if (tokenKey.Length < 64) throw new Exception("Your tokenKey needs to be longer");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        // Creating claims for the token
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // Creating token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = creds
        };

        // Creating token handler and writing the token as string
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}

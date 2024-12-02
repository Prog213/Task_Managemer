using System.Security.Claims;

namespace API.Extensions;

public static class UserClaimsExtension
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        var username = (user.FindFirst(ClaimTypes.Name)?.Value) 
            ?? throw new UnauthorizedAccessException();
        
        return username;
    }
}

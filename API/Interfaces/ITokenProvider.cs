using API.Models;

namespace API.Services.Interfaces;

public interface ITokenProvider
{
    string CreateToken(User user);
}

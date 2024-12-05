using Domain.Models;

namespace Application.Interfaces;

public interface ITokenProvider
{
    string CreateToken(User user);
}

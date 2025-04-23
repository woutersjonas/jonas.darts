using Jonas.Darts.Domain.Models;

namespace Jonas.Darts.Backend.Services.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}

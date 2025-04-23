using Jonas.Darts.Domain.DTOs;

namespace Jonas.Darts.Backend.Services.Interfaces;

public interface IAuthService
{
    Task<(bool Success, string Message)> RegisterAsync(RegisterDTO dto);
    Task<(bool Success, string TokenOrMessage)> LoginAsync(LoginDTO dto);
}

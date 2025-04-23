using Jonas.Darts.Backend.Services.Interfaces;
using Jonas.Darts.Data.Repositories.Interfaces;
using Jonas.Darts.Domain.DTOs;
using Jonas.Darts.Domain.Helpers;
using Jonas.Darts.Domain.Models;

namespace Jonas.Darts.Backend.Services;

public class AuthService: IAuthService
{
    private readonly IAuthRepository _authRepo;
    private readonly ITokenService _tokenService;

    public AuthService(IAuthRepository authRepo, ITokenService tokenService)
    {
        _authRepo = authRepo;
        _tokenService = tokenService;
    }

    public async Task<(bool Success, string Message)> RegisterAsync(RegisterDTO dto)
    {
        /* Validation */
        if (await _authRepo.UserNameExistsAsync(dto.Username))
            return (false, "Username already exists.");

        var (PasswordIsValid, PasswordMessage) = UserAuthValidator.ValidatePassword(dto.Password);
        if (!PasswordIsValid) return (false, PasswordMessage);

        var (UsernameIsValid, UsernameMessage) = UserAuthValidator.ValidateUsername(dto.Username);
        if (!UsernameIsValid) return (false, UsernameMessage);

        var (EmailIsValid, EmailMessage) = UserAuthValidator.ValidateEmail(dto.Email);
        if (!EmailIsValid) return (false, EmailMessage);

        /* Make user */
        var user = new User(dto.Username, dto.Password, dto.Email, dto.Firstname, dto.Lastname);
        await _authRepo.AddUserAsync(user);
        await _authRepo.SaveChangesAsync();

        return (true, "Account aangemaakt!");
    }

    public async Task<(bool Success, string TokenOrMessage)> LoginAsync(LoginDTO dto)
    {
        /* Get user */
        var user = await _authRepo.GetUserByUsernameAsync(dto.Username);

        /* Check hashed password */
        if (user == null || !user.VerifyPassword(dto.Password))
            return (false, "Ongeldige login");

        /* Create token */
        var token = _tokenService.CreateToken(user);

        return (true, token);
    }
}

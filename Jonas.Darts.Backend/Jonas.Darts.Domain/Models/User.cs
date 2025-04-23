using Jonas.Darts.Domain.Helpers;
using Jonas.Darts.Domain.Exceptions;

namespace Jonas.Darts.Domain.Models;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Username { get; private set; } = "";
    public string PasswordHash { get; private set; } = "";
    public string Email { get; private set; } = "";
    public string Firstname { get; private set; } = "";
    public string Lastname { get; private set; } = "";
    public bool IsAdmin { get; private set; } = false;

    private User() { }

    public User(string username, string password, string email, string firstname, string lastname, bool isAdmin = false)
    {
        var resultUserNameValidation = UserAuthValidator.ValidateUsername(username);
        if (!resultUserNameValidation.IsValid)
            throw new UserValidationException(resultUserNameValidation.Message);

        var resultPasswordValidation = UserAuthValidator.ValidatePassword(password);
        if (!resultPasswordValidation.IsValid)
            throw new UserValidationException(resultPasswordValidation.Message);

        var resultEmailValidation = UserAuthValidator.ValidateEmail(email);
        if (!resultEmailValidation.IsValid)
            throw new UserValidationException(resultEmailValidation.Message);


        Id = Guid.NewGuid();
        Username = username;
        PasswordHash = HashPassword(password);
        Email = email;
        Firstname = firstname;
        Lastname = lastname;
        IsAdmin = isAdmin;
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }

    public string? FullName
    {
        get
        {
            if (string.IsNullOrEmpty(Firstname) && string.IsNullOrEmpty(Lastname))
                return null;

            return $"{Firstname} {Lastname}".Trim();
        }
    }
}

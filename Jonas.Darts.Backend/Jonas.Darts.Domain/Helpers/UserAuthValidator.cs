using System.Text.RegularExpressions;

namespace Jonas.Darts.Domain.Helpers;

public static class UserAuthValidator
{
    public static (bool IsValid, string Message) ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return (false, "Wachtwoord mag niet leeg zijn");

        if (password.Length < 8)
            return (false, "Wachtwoord moet minstens 8 tekens bevatten");

        if (!password.Any(char.IsUpper))
            return (false, "Wachtwoord moet minstens één hoofdletter bevatten");

        if (!password.Any(char.IsLower))
            return (false, "Wachtwoord moet minstens één kleine letter bevatten");

        if (!password.Any(char.IsDigit))
            return (false, "Wachtwoord moet minstens één cijfer bevatten");

        if (!password.Any(ch => "!@#$%^&*()_+-=[]{}|;:,.<>?".Contains(ch)))
            return (false, "Wachtwoord moet minstens één speciaal teken bevatten");

        return (true, string.Empty);
    }

    public static (bool IsValid, string Message) ValidateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return (false, "Gebruikersnaam mag niet leeg zijn");

        if (username.Length < 6)
            return (false, "Gebruikersnaam moet minstens 6 tekens bevatten");

        if (!username.All(char.IsLetterOrDigit))
            return (false, "Gebruikersnaam mag alleen letters en cijfers bevatten");

        return (true, string.Empty);
    }

    public static (bool IsValid, string Message) ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return (false, "Email mag niet leeg zijn");

        if (!Regex.Match(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$").Success)
            return (false, "Ongeldig emailadres");

        return (true, string.Empty);
    }
}


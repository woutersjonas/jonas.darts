using FluentAssertions;
using Jonas.Darts.Domain.Helpers;

namespace Jonas.Darts.Test;

public class Helpers_UserAuthValidatorTests
{
    [Theory]
    [InlineData("", false, "Wachtwoord mag niet leeg zijn")]
    [InlineData("short", false, "Wachtwoord moet minstens 8 tekens bevatten")]
    [InlineData("lowercase", false, "Wachtwoord moet minstens één hoofdletter bevatten")]
    [InlineData("UPPERCASE", false, "Wachtwoord moet minstens één kleine letter bevatten")]
    [InlineData("ZeroDigits", false, "Wachtwoord moet minstens één cijfer bevatten")]
    [InlineData("0SpecialCharacters", false, "Wachtwoord moet minstens één speciaal teken bevatten")]
    [InlineData("1ValidPassword!", true, "")]
    public void ValidatePassword(string password, bool expectedIsValid, string expectedMessage)
    {
        // Act
        var (IsValid, Message) = UserAuthValidator.ValidatePassword(password);

        // Assert
        IsValid.Should().Be(expectedIsValid);
        Message.Should().Be(expectedMessage);
    }

    [Theory]
    [InlineData("", false, "Gebruikersnaam mag niet leeg zijn")]
    [InlineData("short", false, "Gebruikersnaam moet minstens 6 tekens bevatten")]
    [InlineData("special_char", false, "Gebruikersnaam mag alleen letters en cijfers bevatten")]
    [InlineData("newUser", true, "")]
    public void ValidateUserName(string username, bool expectedIsValid, string expectedMessage)
    {
        // Act
        var (IsValid, Message) = UserAuthValidator.ValidateUsername(username);

        // Assert
        IsValid.Should().Be(expectedIsValid);
        Message.Should().Be(expectedMessage);
    }
}

using FluentAssertions;
using Jonas.Darts.Domain.Models;
using Jonas.Darts.Backend.Services;
using Microsoft.Extensions.Configuration;

namespace Jonas.Darts.Test;

public class Services_TokenServiceTests
{
    [Fact]
    public void CreateToken_ShouldReturnValidToken()
    {
        // Arrange
        var configData = new Dictionary<string, string>
        {
            {"Jwt:Key", "SuperLongJwtKeyForTestPurposes123456789!" },
            { "Jwt:Issuer", "TestIssuer" },
            { "Jwt:Audience", "TestAudience" }
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configData)
            .Build();

        var service = new TokenService(configuration);

        var user = new User("testUser", "lokC23*-+", "jonas1wouters@hotmail.com", "Jonas", "Wouters");

        // Act
        var token = service.CreateToken(user);

        // Assert
        token.Should().NotBeNullOrEmpty();
    }
}
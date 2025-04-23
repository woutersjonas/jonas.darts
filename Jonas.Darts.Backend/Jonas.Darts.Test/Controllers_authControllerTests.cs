using Jonas.Darts.Backend.Controllers;
using Jonas.Darts.Backend.Services.Interfaces;
using Jonas.Darts.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Jonas.Darts.Test;

public class Controllers_authControllerTests
{
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly AuthController _controller;

    public Controllers_authControllerTests()
    {
        _authServiceMock = new Mock<IAuthService>();
        _controller = new AuthController(_authServiceMock.Object);
    }

    [Fact]
    public async Task Register_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var registerDto = new RegisterDTO { Username = "testUser", Password = "lokC23*-+", Email = "testuser@gmail.com", Firstname = "test", Lastname = "user" };
        _authServiceMock.Setup(s => s.RegisterAsync(registerDto)).ReturnsAsync((true, "Account aangemaakt!"));

        // Act
        var result = await _controller.Register(registerDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Account aangemaakt!", okResult.Value);
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenFailed()
    {
        var dto = new RegisterDTO { Username = "taken", Password = "pass", Email = "test@example.com" };
        _authServiceMock.Setup(s => s.RegisterAsync(dto)).ReturnsAsync((false, "Username already exists"));

        var result = await _controller.Register(dto);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Username already exists", badRequest.Value);
    }

    [Fact]
    public async Task Login_ReturnsOk_WithToken_WhenSuccessful()
    {
        var dto = new LoginDTO { Username = "testuser", Password = "pass" };
        _authServiceMock.Setup(s => s.LoginAsync(dto)).ReturnsAsync((true, "mock-token"));

        var result = await _controller.Login(dto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<LoginResponse>(okResult.Value);
        Assert.Equal("mock-token", response.Token);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenInvalid()
    {
        var dto = new LoginDTO { Username = "wronguser", Password = "wrong" };
        _authServiceMock.Setup(s => s.LoginAsync(dto)).ReturnsAsync((false, "Invalid credentials"));

        var result = await _controller.Login(dto);

        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("Invalid credentials", unauthorizedResult.Value);
    }
}

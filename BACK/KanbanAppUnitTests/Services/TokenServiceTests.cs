using Kanban.API.Interfaces;
using Kanban.API.Models;
using Kanban.API.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace KanbanAppUnitTests.Services;

public class TokenServiceTests
{
    private readonly IConfiguration _config;
    private readonly TokenService _tokenService;

    public TokenServiceTests()
    {
        _config = Substitute.For<IConfiguration>();
        _config["jwt:key"].Returns("2f598abb2b801a9ab5cc41856607512a");
        _config["jwt:ValidIssuer"].Returns("test");
        _tokenService = new TokenService(_config);
    }

    [Fact]
    public void TokenService_BuildToken_ShouldbeSucessfull()
    {
        // Arrange
        
        var loginData = new LoginData() { Login = "test", Senha = "test" };

        // Act
        var response = _tokenService.BuildToken(loginData);

        // Assert
        Assert.NotEmpty(response);
    }


    [Fact]
    public void TokenService_GenerateClaims_ShouldbeSucessfull()
    {
        // Arrange
        var loginData = new LoginData() { Login = "test", Senha = "test" };

        // Act
        var response = _tokenService.GenerateClaims(loginData);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public void TokenService_IsTokenValid_ShouldbeSucessfull()
    {
        // Arrange
        var loginData = new LoginData() { Login = "test", Senha = "test" };

        var token = _tokenService.BuildToken(loginData);
        // Act
        var response = _tokenService.IsTokenValid(token);

        // Assert
        Assert.True(response);
    }

    [Fact]
    public void TokenService_IsTokenValid_ShouldFail()
    {
        // Arrange
        var loginData = new LoginData() { Login = "test", Senha = "test" };

        // Act
        var response = _tokenService.IsTokenValid("token");

        // Assert
        Assert.False(response);
    }

    [Fact]
    public void TokenService_IsTokenValid_ShouldFailEmpty()
    {
        // Arrange
        var loginData = new LoginData() { Login = "test", Senha = "test" };

        // Act
        var response = _tokenService.IsTokenValid("");

        // Assert
        Assert.False(response);
    }
}

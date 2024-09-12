using Kanban.API.Cards.Queries;
using Kanban.API.Models;
using Kanban.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace KanbanAppUnitTests.Services;

public class LoginServiceTests
{
    private readonly IConfiguration _config;
    private readonly ILogger<LoginService> _logger;
    private readonly LoginService _loginService;

    public LoginServiceTests()
    {
        _config = Substitute.For<IConfiguration>();
        _logger = Substitute.For<ILogger<LoginService>>();
        _loginService = new LoginService(_config, _logger);
    }

    [Fact]
    public void LoginService_Login_ShouldbeSucessfull()
    {
        // Arrange
        _config["User:Login"].Returns("test");
        _config["User:Senha"].Returns("test");
        var loginData = new LoginData() { Login = "test", Senha = "test" };

        // Act
        var response = _loginService.Login(loginData);

        // Assert
        Assert.True(response);
    }


    [Fact]
    public void LoginService_Login_ShouldFail()
    {
        // Arrange
        _config["User:Login"].Returns("test");
        _config["User:Senha"].Returns("test");
        var loginData = new LoginData() { Login = "test1", Senha = "test1" };

        // Act
        var response = _loginService.Login(loginData);

        // Assert
        Assert.False(response);
    }
}

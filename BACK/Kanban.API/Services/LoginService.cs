using Kanban.API.Interfaces;
using Kanban.API.Models;

namespace Kanban.API.Services;

public class LoginService : ILoginService
{
    private readonly IConfiguration _config;
    private readonly ILogger<LoginService> _logger;

    public LoginService(IConfiguration config, ILogger<LoginService> logger)
    {
        _config = config;
        _logger = logger;
    }
    public bool Login(LoginData loginData)
    {
        if(loginData.Login == _config["User:Login"] && loginData.Senha == _config["User:Senha"])
        {
            _logger.LogInformation("User Sucessfull login!");
            return true;
        }
        return false;
    }
}

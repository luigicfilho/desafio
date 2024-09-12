using Kanban.API.Interfaces;
using Kanban.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.API.EndpointsDefinition;

public class LoginEndpointDefinition : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var login = app.MapGroup("/login").WithTags("Login");

        login.MapPost("/", GenToken)
             .WithName("Login")
             .WithOpenApi();
    }

    internal async Task<IResult> GenToken([FromServices] ITokenService tokenService, 
                                          [FromServices] ILoginService loginService, 
                                          [FromBody] LoginData login)
    {
        if (loginService.Login(login)) {
            var token = tokenService.BuildToken(login);
            return Results.Ok(token);
        }
        else
        {
            return Results.BadRequest();
        }
    }
}

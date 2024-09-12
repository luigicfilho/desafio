using Kanban.API.Models;
using System.Security.Claims;

namespace Kanban.API.Interfaces;

public interface ITokenService
{
    string BuildToken(LoginData loginData);
    public ClaimsIdentity GenerateClaims(LoginData loginData);
    bool IsTokenValid(string token);
}

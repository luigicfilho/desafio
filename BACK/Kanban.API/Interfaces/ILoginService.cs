using Kanban.API.Models;

namespace Kanban.API.Interfaces
{
    public interface ILoginService
    {
        bool Login(LoginData loginData);
    }
}

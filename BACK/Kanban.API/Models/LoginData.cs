using System.ComponentModel.DataAnnotations;

namespace Kanban.API.Models;

public class LoginData
{
    [Key]
    public required string Login { get; set; }
    public required string Senha { get; set; }
}

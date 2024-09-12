namespace Kanban.API.Models;

public class Card
{
    public Guid Id { get; set; }
    public required string Titulo { get; set; }
    public required string Conteudo { get; set; }
    public required string Lista { get; set; }
}

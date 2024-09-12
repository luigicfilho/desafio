using Kanban.API.Models;
using MediatR;

namespace Kanban.API.Cards.Commands
{
    public class UpdateCardCommand : IRequest<Card>
    {
        public Guid Id { get; set; }
        public required string Titulo { get; set; }
        public required string Conteudo { get; set; }
        public required string Lista { get; set; }
    }
}

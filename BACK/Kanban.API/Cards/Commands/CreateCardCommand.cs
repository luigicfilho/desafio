using Kanban.API.Models;
using MediatR;

namespace Kanban.API.Cards.Commands
{
    public class CreateCardCommand : IRequest<Card>
    {
        public Guid Id = Guid.NewGuid();
        public required string Titulo { get; set; }
        public required string Conteudo { get; set; }
        public required string Lista { get; set; }

    }
}

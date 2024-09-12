using Kanban.API.Models;
using MediatR;

namespace Kanban.API.Cards.Commands
{
    public class DeleteCardCommand : IRequest<IList<Card>>
    {
        public Guid Id { get; set; }
    }
}

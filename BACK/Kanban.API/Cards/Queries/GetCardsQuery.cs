using Kanban.API.Models;
using MediatR;

namespace Kanban.API.Cards.Queries
{
    public class GetCardsQuery : IRequest<IList<Card>>
    {
    }
}

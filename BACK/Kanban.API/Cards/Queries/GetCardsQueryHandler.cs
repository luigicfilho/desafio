using Kanban.API.Interfaces;
using Kanban.API.Models;
using MediatR;

namespace Kanban.API.Cards.Queries
{
    public class GetCardsQueryHandler : IRequestHandler<GetCardsQuery, IList<Card>>
    {
        private readonly ICardsRepository _cardRepository;

        public GetCardsQueryHandler(ICardsRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public Task<IList<Card>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
        {
            return _cardRepository.GetAllAsync();
        }
    }
}

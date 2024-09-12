using Kanban.API.Interfaces;
using Kanban.API.Models;
using MediatR;

namespace Kanban.API.Cards.Commands
{
    public class DeleteCardHandler : IRequestHandler<DeleteCardCommand, IList<Card>>
    {
        private readonly ICardsRepository _cardRepository;
        private readonly ILogger<DeleteCardHandler> _logger;

        public DeleteCardHandler(ICardsRepository cardRepository, ILogger<DeleteCardHandler> logger)
        {
            _cardRepository = cardRepository;
            _logger = logger;
        }
        public Task<IList<Card>> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
        {
            var card = _cardRepository.GetByIdAsync(request.Id, default).Result;

            _cardRepository.Remove(card);

            var value = _cardRepository.SaveAsync();

            if (value.IsCompletedSuccessfully == true)
            {
                _logger.LogInformation($"{DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss")} - Card {card.Id} - {card.Titulo} - Removido");
                var returned = _cardRepository.GetAllAsync();
                return returned;
            }
            else
            {
                return null;
            }
        }
    }
}

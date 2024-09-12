using Kanban.API.Interfaces;
using Kanban.API.Models;
using MediatR;

namespace Kanban.API.Cards.Commands
{
    public class UpdateCardHandler : IRequestHandler<UpdateCardCommand, Card>
    {
        private readonly ICardsRepository _cardRepository;
        private readonly ILogger<UpdateCardHandler> _logger;
        public UpdateCardHandler(ICardsRepository cardRepository, ILogger<UpdateCardHandler> logger)
        {
            _cardRepository = cardRepository;
            _logger = logger;
        }
        public Task<Card> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
        {
            var card = _cardRepository.GetByIdAsync(request.Id).Result;
            if(card == null)
            {
                return null;
            }
            card.Conteudo = request.Conteudo;
            card.Titulo = request.Titulo;
            card.Lista = request.Lista;

            _cardRepository.UpdateAsync(card);

            var value = _cardRepository.SaveAsync();

            _logger.LogInformation($"{DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss")} - Card {card.Id} - {card.Titulo} - Alterado");

            return Task.FromResult(card);
        }
    }
}

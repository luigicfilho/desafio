using Kanban.API.Interfaces;
using Kanban.API.Models;
using MediatR;

namespace Kanban.API.Cards.Commands
{
    public class CreateCardHandler : IRequestHandler<CreateCardCommand, Card>
    {
        private readonly ICardsRepository _cardRepository;

        public CreateCardHandler(ICardsRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public Task<Card> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            var card = new Card()
            {
                Id = request.Id,
                Titulo = request.Titulo,
                Conteudo = request.Conteudo,
                Lista = request.Lista
            };

            _cardRepository.AddAsync(card);
            var value = _cardRepository.SaveAsync();

             if(value.IsCompletedSuccessfully == true)
             {
                return Task.FromResult(card);
             }
             else
             {
                return null;
             }
        }
    }
}

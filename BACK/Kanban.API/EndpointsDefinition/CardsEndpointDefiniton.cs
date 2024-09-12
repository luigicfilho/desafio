using Kanban.API.Cards.Commands;
using Kanban.API.Cards.Queries;
using Kanban.API.Interfaces;
using Kanban.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.API.EndpointsDefinition
{
    public class CardsEndpointDefiniton : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var cards = app.MapGroup("/cards").WithTags("User Operations");

            cards.MapGet("/", GetCards)
                 .WithName("GetCards")
                 .RequireAuthorization()
                 .WithOpenApi();

            cards.MapPost("/", CreateCard)
                 .WithName("PostCards")
                 .RequireAuthorization()
                 .WithOpenApi();

            cards.MapPut("/{id:guid}", UpdateCard)
                 .WithName("PutCards")
                 .RequireAuthorization()
                 .WithOpenApi();

            cards.MapDelete("/{id:guid}", DeleteCard)
                 .WithName("DeleteCards")
                 .RequireAuthorization()
                 .WithOpenApi();
        }

        internal async Task<IResult> GetCards([FromServices] IMediator mediator)
        {
            var cards= await mediator.Send(new GetCardsQuery());
            return cards != null ? Results.Ok(cards) : Results.NotFound();
        }
        internal async Task<IResult> CreateCard([FromServices] IMediator mediator, [FromBody] CardRequest card)
        {
            var result = await mediator.Send(new CreateCardCommand() { Conteudo = card.Conteudo, Lista = card.Lista, Titulo = card.Titulo});
            return result != null ? Results.Ok(result) : Results.BadRequest();
        }

        internal async Task<IResult> UpdateCard([FromServices] IMediator mediator, Guid id, [FromBody] CardRequest card)
        {
            //Caso o id não exista retorne 404
            var result = await mediator.Send(new UpdateCardCommand() { Id = id, Titulo = card.Titulo, Conteudo = card.Conteudo, Lista = card.Lista});
            return result != null ? Results.Ok(result) : Results.BadRequest();
        }

        internal async Task<IResult> DeleteCard([FromServices] IMediator mediator, Guid id)
        {
            //Caso o id não exista retorne 404
            var result = await mediator.Send(new DeleteCardCommand() { Id = id});
            return result != null ? Results.Ok(result) : Results.BadRequest();
        }
    }
}

using Kanban.API.Cards.Commands;
using Kanban.API.Interfaces;
using Kanban.API.Models;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace KanbanAppUnitTests.Cards.Commands;

public class UpdateCardTests
{
    private readonly ICardsRepository _dbContext;
    private readonly UpdateCardHandler _updateCardHandler;
    private readonly ILogger<UpdateCardHandler> _logger;
    public UpdateCardTests()
    {
        _dbContext = Substitute.For<ICardsRepository>();
        _logger = Substitute.For<ILogger<UpdateCardHandler>>();
        _updateCardHandler = new UpdateCardHandler(_dbContext, _logger);
    }

    [Fact]
    public async void UpdateCard_ShouldbeSucessfull()
    {
        // Arrange
        var Id = Guid.NewGuid();
        var card = new Card() { 
            Id = Id, 
            Conteudo = "x",
            Titulo = "y",
            Lista = "z"
        };
        _dbContext.SaveAsync().Returns(Task.CompletedTask);
        _dbContext.GetByIdAsync(Id).Returns(card);
        var command = new UpdateCardCommand()
        {
            Id = Id,
            Titulo = "Test",
            Conteudo = "Test1",
            Lista = "List"
        };

        // Act
        var response = await _updateCardHandler.Handle(command, default);

        // Assert
        Assert.Equivalent(typeof(Card), response.GetType());
    }

    [Fact]
    public async void UpdateCard_ShouldbeFail()
    {
        // Arrange
        var Id = Guid.NewGuid();
        var card = new Card()
        {
            Id = Id,
            Conteudo = "x",
            Titulo = "y",
            Lista = "z"
        };
        var command = new UpdateCardCommand()
        {
            Id = Id,
            Titulo = "Test",
            Conteudo = "Test1",
            Lista = "List"
        };
        _dbContext.GetByIdAsync(Id).ReturnsNull();
        _dbContext.SaveAsync().Throws(new Exception());
        // Act
        // Assert
        Assert.Null(_updateCardHandler.Handle(command, default));
        //await Assert.ThrowsAsync<Exception>(async () => { await _updateCardHandler.Handle(command, default); });
    }
}

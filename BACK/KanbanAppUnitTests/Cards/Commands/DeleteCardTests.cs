using Kanban.API.Cards.Commands;
using Kanban.API.Interfaces;
using Kanban.API.Models;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace KanbanAppUnitTests.Cards.Commands;

public class DeleteCardTests
{
    private readonly ICardsRepository _dbContext;
    private readonly DeleteCardHandler _deleteCardHandler;
    private readonly ILogger<DeleteCardHandler> _logger;
    public DeleteCardTests()
    {
        _dbContext = Substitute.For<ICardsRepository>();
        _logger = Substitute.For<ILogger<DeleteCardHandler>>();
        _deleteCardHandler = new DeleteCardHandler(_dbContext, _logger);
    }

    [Fact]
    public async void DeleteCard_ShouldbeSucessfull()
    {
        // Arrange
        var Id = Guid.NewGuid();
        _dbContext.SaveAsync().Returns(Task.CompletedTask);
        _dbContext.GetAllAsync().Returns(new List<Card>());
        var card = new Card()
        {
            Id = Id,
            Conteudo = "x",
            Titulo = "y",
            Lista = "z"
        };
        var command = new DeleteCardCommand()
        {
            Id = Id
        };
        _dbContext.GetByIdAsync(Id, default).Returns(card);

        // Act
        var response = await _deleteCardHandler.Handle(command, default);

        // Assert
        Assert.True(response.GetType() == typeof(List<Card>));
    }

    [Fact]
    public async void DeleteCard_ShouldbeFail()
    {
        // Arrange
        var Id = Guid.NewGuid();
        Task failedTask = Task.FromException(new Exception());
        var command = new DeleteCardCommand()
        {
            Id = Id
        };
        _dbContext.SaveAsync().Returns(failedTask);
        // Act
        // Assert
        Assert.Null(_deleteCardHandler.Handle(command, default));
    }
}

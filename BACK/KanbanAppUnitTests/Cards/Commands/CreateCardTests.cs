using Kanban.API.Cards.Commands;
using Kanban.API.Interfaces;
using Kanban.API.Models;
using NSubstitute;

namespace KanbanAppUnitTests.Cards.Commands;

public class CreateCardTests
{
    private readonly ICardsRepository _dbContext;
    private readonly CreateCardHandler _createCardHandler;
    public CreateCardTests()
    {
        _dbContext = Substitute.For<ICardsRepository>();
        _createCardHandler = new CreateCardHandler(_dbContext);
    }

    [Fact]
    public async void CreateCard_ShouldbeSucessfull()
    {
        // Arrange

        _dbContext.SaveAsync().Returns(Task.CompletedTask);
        var command = new CreateCardCommand()
        {
            Titulo = "Test",
            Conteudo = "Test1",
            Lista = "List"
        };

        // Act
        var response = await _createCardHandler.Handle(command, default);

        // Assert
        Assert.Equivalent(typeof(Card), response.GetType());
    }

    [Fact]
    public async void CreateCard_ShouldbeFail()
    {
        // Arrange
        Task failedTask = Task.FromException(new Exception());
        var command = new CreateCardCommand()
        {
            Titulo = "Test",
            Conteudo = "Test1",
            Lista = "List"
        };
        _dbContext.SaveAsync().Returns(failedTask);
        // Act
        // Assert
        Assert.Null(_createCardHandler.Handle(command, default));
    }
}

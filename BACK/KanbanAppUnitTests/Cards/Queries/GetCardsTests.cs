using Kanban.API.Cards.Queries;
using Kanban.API.Interfaces;
using Kanban.API.Models;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace KanbanAppUnitTests.Cards.Queries;

public class GetCardsTests
{
    private readonly ICardsRepository _dbContext;
    private readonly GetCardsQueryHandler _getCardHandler;
    public GetCardsTests()
    {
        _dbContext = Substitute.For<ICardsRepository>();
        _getCardHandler = new GetCardsQueryHandler(_dbContext);
    }

    [Fact]
    public async void GetCards_ShouldbeSucessfull()
    {
        // Arrange

        _dbContext.GetAllAsync().Returns(new List<Card>());
        var query = new GetCardsQuery();

        // Act
        var response = await _getCardHandler.Handle(query, default);

        // Assert
        Assert.Equivalent(typeof(List<Card>), response.GetType());
    }

    [Fact]
    public async void GetCards_ShouldbeFail()
    {
        // Arrange
        _dbContext.GetAllAsync().ThrowsAsync(new Exception());
        var query = new GetCardsQuery();
        // Act
        // Assert
        await Assert.ThrowsAsync<Exception>(async () => { await _getCardHandler.Handle(query, default); });
    }
}

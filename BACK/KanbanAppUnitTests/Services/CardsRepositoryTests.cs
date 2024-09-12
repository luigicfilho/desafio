using Kanban.API.Data;
using Kanban.API.Models;
using Kanban.API.Services;
using Microsoft.EntityFrameworkCore;

namespace KanbanAppUnitTests.Services;

public class CardsRepositoryTests
{
    public CardsRepositoryTests()
    {
    }

    [Fact]
    public async void CardRepository_GetAllAsync_ShouldbeSucessfull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<KanbanAppDbContext>()
            .UseInMemoryDatabase(databaseName: "KanbanTestDb")
            .Options;

        using (var context = new KanbanAppDbContext(options))
        {
            context.Cards.Add(new Card { Id = Guid.NewGuid(), Titulo = "Card 1", Conteudo = "Content 1", Lista = "List 1" });
            context.SaveChanges();

            var repository = new CardsRepository(context);

            // Act
            var cards = await repository.GetAllAsync();

            // Assert
            Assert.Equal("Card 1", cards.First().Titulo);
        }
    }


    [Fact]
    public async void CardRepository_GetByIdAsync_ShouldbeSucessfull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<KanbanAppDbContext>()
            .UseInMemoryDatabase(databaseName: "KanbanTestDb")
            .Options;

        using (var context = new KanbanAppDbContext(options))
        {
            var id = Guid.NewGuid();
            context.Cards.Add(new Card { Id = id, Titulo = "Card 1", Conteudo = "Content 1", Lista = "List 1" });
            context.SaveChanges();

            var repository = new CardsRepository(context);

            // Act
            var cards = await repository.GetByIdAsync(id);

            // Assert
            Assert.Equal(id, cards.Id);
            Assert.Equal("Card 1", cards.Titulo);
        }
    }
}

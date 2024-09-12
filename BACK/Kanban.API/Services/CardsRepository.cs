using Kanban.API.Data;
using Kanban.API.Interfaces;
using Kanban.API.Models;
using Kanban.API.Services.BaseRepository;

namespace Kanban.API.Services;

public class CardsRepository : BaseRepository<Card, KanbanAppDbContext>, ICardsRepository
{
    public CardsRepository(KanbanAppDbContext? dbContext): base(dbContext)
    {
    }

    public async Task<IList<Card>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IList<Card> result = await GetAsync<Card>(cancellationToken: cancellationToken).ConfigureAwait(false);
        return result;
    }

    public async Task<Card?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetSingleOrDefaultAsync<Card>(std => std.Id == id, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
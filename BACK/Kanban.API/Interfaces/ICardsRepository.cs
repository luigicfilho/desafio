using Kanban.API.Models;
using Kanban.API.Services.BaseRepository;

namespace Kanban.API.Interfaces
{
    public interface ICardsRepository : IBaseRepository<Card>
    {
        Task<IList<Card>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Card?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

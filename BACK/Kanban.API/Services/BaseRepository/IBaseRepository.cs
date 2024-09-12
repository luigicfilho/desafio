using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Kanban.API.Services.BaseRepository;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<IList<TResult>> GetAsync<TResult>(Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, TResult>>? selector = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool tracking = false,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetSingleOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool tracking = false,
        CancellationToken cancellationToken = default);

    Task SaveAsync(CancellationToken cancellationToken = default);

    bool Remove(TEntity entity, bool tracking = true);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
}

using Value.Objects.Helper.Abstractions;
using Shared.Common.Helper.ErrorsHandler;

namespace Permissions.Domain.Abstractions.Common;

public interface ISingleQueriesGenericRepository<TEntity, TId>
        where TEntity : class
        where TId : notnull
{
    /// <summary>
    /// Get a <see cref="TEntity"/> by <see cref="BaseId"/>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<TEntity>> ByIdAsync(TId id, CancellationToken cancellationToken = default);
}
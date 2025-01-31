namespace Permissions.Domain.Abstractions.Common;

public interface ICollectionQueriesGenericRepository<TEntity>
        where TEntity : class
{
    /// <summary>
    /// Get a <see cref="TEntity"/> collection
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<(IReadOnlyCollection<TEntity>, int, int)> CollectionAsync(int pageNumber, CancellationToken cancellationToken = default);
}
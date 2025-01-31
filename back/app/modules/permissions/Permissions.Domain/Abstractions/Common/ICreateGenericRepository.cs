namespace Permissions.Domain.Abstractions.Common;

public interface ICreateGenericRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Create a new <see cref="TEntity"/>
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> CreateAsync(TEntity model, CancellationToken cancellationToken);
}
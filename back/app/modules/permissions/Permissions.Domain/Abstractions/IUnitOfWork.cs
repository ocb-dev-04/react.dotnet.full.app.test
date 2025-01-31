using Permissions.Domain.Abstractions.Repositories;

namespace Permissions.Domain.Abstractions;

public interface IUnitOfWork
{
    /// <summary>
    /// <see cref="IPermissionRepository"/> contract and implementation
    /// </summary>
    IPermissionRepository Permission { get; }

    /// <summary>
    /// <see cref="IPermissionTypeRepository"/> contract and implementation
    /// </summary>
    IPermissionTypeRepository PermissionType { get; }

    /// <summary>
    /// Save all changes
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CommitAsync(CancellationToken cancellationToken = default);
}

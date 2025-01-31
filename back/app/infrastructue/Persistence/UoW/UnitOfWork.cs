using Persistence.Context;
using Persistence.Repositories;
using Permissions.Domain.Abstractions;
using Permissions.Domain.Abstractions.Repositories;

namespace Persistence.UoW;

internal sealed class UnitOfWork 
    : IUnitOfWork,
        IAsyncDisposable
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

        _dbContext = dbContext;
    }

    #region Private

    private IPermissionRepository? _permissionRepository;
    private IPermissionTypeRepository? _permissionTypeRepository;

    #endregion

    #region Public

    public IPermissionRepository Permission
    {
        get => _permissionRepository ??= new PermissionRepository(_dbContext);
    }

    public IPermissionTypeRepository PermissionType
    {
        get => _permissionTypeRepository ??= new PermissionTypeRepository(_dbContext);
    }

    /// <inheritdoc/>
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContext.ChangeTracker.HasChanges())
            await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        if (_dbContext is not null)
            await _dbContext.DisposeAsync();
    }

    #endregion
}
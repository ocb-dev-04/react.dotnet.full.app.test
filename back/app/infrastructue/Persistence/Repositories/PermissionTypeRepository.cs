using Persistence.Context;
using Permissions.Domain.Errors;
using Permissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Helper.ErrorsHandler;
using Permissions.Domain.Abstractions.Repositories;

namespace Persistence.Repositories;

internal sealed class PermissionTypeRepository
    : IPermissionTypeRepository
{
    private readonly DbSet<PermissionType> _table;

    public PermissionTypeRepository(AppDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

        _table = dbContext.Set<PermissionType>();
    }

    /// <inheritdoc/>
    public async Task<Result<PermissionType>> ByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        PermissionType? found = await _table.FindAsync(id, cancellationToken);
        if (found is null)
            return Result.Failure<PermissionType>(PermissionTypeErrors.NotFound);

        return found;
    }

    /// <inheritdoc/>
    public async Task<PermissionType> CreateAsync(PermissionType model, CancellationToken cancellationToken)
         => (await _table.AddAsync(model, cancellationToken)).Entity;
}

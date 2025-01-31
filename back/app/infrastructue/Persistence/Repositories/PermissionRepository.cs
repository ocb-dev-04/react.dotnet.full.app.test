using Persistence.Context;
using Permissions.Domain.Errors;
using Permissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Common.Helper.ErrorsHandler;
using Persistence.Repositories.CompiledQueries;
using Permissions.Domain.Abstractions.Repositories;
using Shared.Common.Helper.Abstractions.Entities;

namespace Persistence.Repositories;

internal sealed class PermissionRepository
    : PermissionCompiledQueries,
        IPermissionRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<Permission> _table;

    public PermissionRepository(AppDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

        _dbContext = dbContext;
        _table = dbContext.Set<Permission>();
    }

    /// <inheritdoc/>
    public async Task<Result<Permission>> ByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        Permission? found = await _table.FindAsync(id, cancellationToken);
        if (found is null)
            return Result.Failure<Permission>(PermissionErrors.NotFound);

        return found;
    }

    /// <inheritdoc/>
    public async Task<(IReadOnlyCollection<Permission>, int, int)> CollectionAsync(int pageNumber, CancellationToken cancellationToken = default)
    {
        List<Permission> collection = new();
        await foreach (Permission item in GetCollection(_dbContext, pageNumber))
            collection.Add(item);

        int totalItems = await GetTotalCount(_dbContext);
        int totalPages = (int)Math.Ceiling(totalItems / 10.0);

        return (collection, totalItems, totalPages);
    }

    /// <inheritdoc/>
    public async Task<Permission> CreateAsync(Permission model, CancellationToken cancellationToken)
         => (await _table.AddAsync(model, cancellationToken)).Entity;

    /// <inheritdoc/>
    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        Permission? found = await _table.FindAsync(id, cancellationToken);
        if (found is null)
            return Result.Failure(PermissionErrors.NotFound);

        _table.Remove(found);

        return Result.Success();
    }
}

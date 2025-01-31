using Persistence.Context;
using System.Linq.Expressions;
using Permissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.CompiledQueries;

internal class PermissionCompiledQueries
{
    protected static readonly int _pageSize = 10;

    protected static readonly Func<AppDbContext, Expression<Func<Permission, bool>>, Task<bool>> AnyFilter =
        EF.CompileAsyncQuery(
            (AppDbContext context, Expression<Func<Permission, bool>> filter)
                => context.Set<Permission>().AsNoTracking().Any(filter));

    protected static readonly Func<AppDbContext, int, IAsyncEnumerable<Permission>> GetCollection =
        EF.CompileAsyncQuery(
            (AppDbContext context, int pageNumber)
                => context.Set<Permission>()
                    .Include(i => i.PermissionType)
                    .OrderByDescending(w => w.Id)
                    .Skip((pageNumber - 1) * _pageSize)
                    .Take(_pageSize));

    protected static readonly Func<AppDbContext, Task<int>> GetTotalCount =
        EF.CompileAsyncQuery(
            (AppDbContext context)
                => context.Set<Permission>()
                    .Select(s => s.Id)
                    .Count());
}

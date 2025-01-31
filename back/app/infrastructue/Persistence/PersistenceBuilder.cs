using Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class PersistenceBuilder
{
    public static void CheckMigrations(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        using AppDbContext? context = scope.ServiceProvider.GetService<AppDbContext>();
        ArgumentNullException.ThrowIfNull(context, nameof(context));

        bool canConnect = context.Database.CanConnect();
        if (!canConnect)
            throw new Exception("Can't connect to database");

        IEnumerable<string> pendingMigrations = context.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
            context.Database.Migrate();
    }
}

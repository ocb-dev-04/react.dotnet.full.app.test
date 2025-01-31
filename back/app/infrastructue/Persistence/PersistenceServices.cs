using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Persistence.UoW;
using Persistence.Context;
using Persistence.Settings;
//using Persistence.PreCopiledEntities;
using Permissions.Domain.Abstractions;

namespace Persistence;

public static class PersistenceServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRelationalDatabase();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddRelationalDatabase(this IServiceCollection services)
    {
        services.AddOptions<RelationalDatabaseSettings>()
            .BindConfiguration(nameof(RelationalDatabaseSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext<AppDbContext>((servicesProvider, optionsBuilder) =>
        {
            IOptions<RelationalDatabaseSettings>? databaseOptionsSetup = servicesProvider.GetService<IOptions<RelationalDatabaseSettings>>();
            ArgumentNullException.ThrowIfNull(databaseOptionsSetup, nameof(databaseOptionsSetup));

            RelationalDatabaseSettings databaseOptions = databaseOptionsSetup.Value;
            ArgumentNullException.ThrowIfNullOrEmpty(databaseOptions.ConnectionString, nameof(databaseOptions.ConnectionString));

            optionsBuilder.UseSqlServer(
                databaseOptions.ConnectionString, 
                serverOptions =>
                {
                    serverOptions.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    serverOptions.CommandTimeout(databaseOptions.CommandTimeout);

                    serverOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    serverOptions.MigrationsHistoryTable("_MigrationsHistory", schema: "migrations");
                });

            //optionsBuilder.UseModel(AppDbContextModel.Instance);

#if DEBUG
            optionsBuilder.EnableDetailedErrors(true);
            optionsBuilder.EnableSensitiveDataLogging(true);
#endif
        });

        return services;
    }

}
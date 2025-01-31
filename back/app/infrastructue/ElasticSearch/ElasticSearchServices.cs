using ElasticSearch.Settings;
using ElasticSearch.Abstractions;
using ElasticSearch.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace ElasticSearch;

public static class ElasticSearchServices
{
    public static IServiceCollection AddElasticSearchService(this IServiceCollection services)
    {
        services.AddOptions<ElasticSettings>()
            .BindConfiguration(nameof(ElasticSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton(typeof(IElasticSearchService<>), typeof(ElasticSearchService<>));

        return services;
    }
}

using Persistence;
using Presentation;
using ElasticSearch;
using Shared.Common.Helper;
using Microsoft.AspNetCore.ResponseCompression;

namespace Api;

public static class Services
{
    /// <summary>
    /// Public extension method to inject all services
    /// </summary>
    /// <param name="builder"></param>
    public static void AddServices(this WebApplicationBuilder builder)
    {
        IServiceCollection services = builder.Services;
        IConfiguration configuration = builder.Configuration;

        services.AddControllers();
        services.AddEndpointsApiExplorer()
            .AddHttpContextAccessor();

        services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
            options.EnableForHttps = true;
        });

        services.AddCors(options =>
        {
            string? clientUrl = configuration.GetValue<string>("ClientUrl");
            ArgumentNullException.ThrowIfNull(clientUrl, nameof(clientUrl));

            options.AddPolicy(
                name: "AllowAnyOrigin",
                policy =>
                {
                    policy.WithOrigins(clientUrl)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
        });

        services.AddPersistenceServices(configuration)
            .AddPresentationServices(configuration)
            .AddElasticSearchService()
            .AddSharedCommonProviders()
            .AddSwaggerGen();
    }
}

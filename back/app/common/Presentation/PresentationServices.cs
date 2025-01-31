using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using FluentValidation;
using Presentation.Behaviors;
using Permissions.Application;

namespace Presentation;

/// <summary>
/// Add app services to <see cref="IServiceCollection"/>
/// </summary>
public static class Services
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiBehaviorOptions>(
            options => options.SuppressModelStateInvalidFilter = true);

        Assembly applicationAssembly = typeof(ApplicationReference).Assembly;

        services.AddValidatorsFromAssembly(
            applicationAssembly,
            includeInternalTypes: true);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(applicationAssembly);

            config.AddOpenBehavior(typeof(EventsPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        return services;
    }

}
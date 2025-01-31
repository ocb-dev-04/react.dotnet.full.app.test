using MediatR;
using Shared.Common.Helper.Abstractions.Providers;

namespace Presentation.Behaviors;

public sealed class EventsPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
{
    private readonly IPublisher _publisher;
    private readonly IEntitiesEventsManagementProvider _entitiesEventsManagementProvider;

    public EventsPipelineBehavior(
        IPublisher publisher, 
        IEntitiesEventsManagementProvider entitiesEventsManagementProvider)
    {
        ArgumentNullException.ThrowIfNull(publisher, nameof(publisher));
        ArgumentNullException.ThrowIfNull(entitiesEventsManagementProvider, nameof(entitiesEventsManagementProvider));

        _publisher = publisher;
        _entitiesEventsManagementProvider = entitiesEventsManagementProvider;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (IsNotCommand()) return await next();

        TResponse? response = await next();

        IReadOnlyList<INotification> events = _entitiesEventsManagementProvider.GetDomainEvents();

        foreach (INotification item in events)
            await _publisher.Publish(item, cancellationToken);

        _entitiesEventsManagementProvider.ClearEvents();

        return response;
    }

    public static bool IsNotCommand()
        => !typeof(TRequest).Name.EndsWith("Command");
}

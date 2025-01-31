using MediatR;
using Permissions.Domain.Events;
using ElasticSearch.Abstractions;
using Permissions.Domain.Entities;
using Permissions.Domain.Abstractions;
using Shared.Common.Helper.ErrorsHandler;
using Permissions.Application.UseCases.Permissions;

namespace Permissions.Application.EventsHandler;

internal sealed class PermissionCreatedOrModifiedDomainEventHandler
    : INotificationHandler<PermissionCreatedOrModifiedDomainEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IElasticSearchService<PermissionResponse> _elasticSearchService;

    public PermissionCreatedOrModifiedDomainEventHandler(
        IUnitOfWork unitOfwork,
        IElasticSearchService<PermissionResponse> elasticSearchService)
    {
        ArgumentNullException.ThrowIfNull(unitOfwork, nameof(unitOfwork));
        ArgumentNullException.ThrowIfNull(elasticSearchService, nameof(elasticSearchService));

        _unitOfWork = unitOfwork;
        _elasticSearchService = elasticSearchService;
    }
    public async Task Handle(PermissionCreatedOrModifiedDomainEvent notification, CancellationToken cancellationToken)
    {
        Result<Permission> permissionFound = await _unitOfWork.Permission.ByIdAsync(notification.Id, cancellationToken);
        if (permissionFound.IsFailure) return;

        await _elasticSearchService
            .AddOrUpdateAsync(
                permissionFound.Value.Id.ToString(),
                PermissionResponse.MapFromEntity(permissionFound.Value),
                nameof(PermissionResponse),
                cancellationToken);
    }
}

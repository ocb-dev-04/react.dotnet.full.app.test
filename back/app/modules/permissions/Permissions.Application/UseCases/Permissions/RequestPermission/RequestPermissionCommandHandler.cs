using Permissions.Domain.Entities;
using Permissions.Domain.Abstractions;
using Shared.Common.Helper.ErrorsHandler;
using CQRS.MediatR.Helper.Abstractions.Messaging;
using Shared.Common.Helper.Abstractions.Providers;

namespace Permissions.Application.UseCases.Permissions;

internal sealed class RequestPermissionCommandHandler
    : ICommandHandler<RequestPermissionCommand, PermissionResponse>
{
    private readonly IUnitOfWork _unitOfwork;
    private readonly IEntitiesEventsManagementProvider _entitiesEventsManagementProvider;

    public RequestPermissionCommandHandler(
        IUnitOfWork unitOfwork,
        IEntitiesEventsManagementProvider entitiesEventsManagementProvider)
    {
        ArgumentNullException.ThrowIfNull(unitOfwork, nameof(unitOfwork));
        ArgumentNullException.ThrowIfNull(entitiesEventsManagementProvider, nameof(entitiesEventsManagementProvider));

        _unitOfwork = unitOfwork;
        _entitiesEventsManagementProvider = entitiesEventsManagementProvider;
    }

    public async Task<Result<PermissionResponse>> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
    {
        PermissionType permissionType = new PermissionTypeBuilder()
            .SetDescription(request.Description)
            .Build(_entitiesEventsManagementProvider);
        PermissionType permissionTypeCreated = await _unitOfwork.PermissionType.CreateAsync(permissionType, cancellationToken);

        Permission permission = new PermissionBuilder()
            .SetName(request.EmployeeName)
            .SetLastName(request.EmployeeLastName)
            .SetType(permissionTypeCreated)
            .Build(_entitiesEventsManagementProvider);
        Permission permissionCreated = await _unitOfwork.Permission.CreateAsync(permission, cancellationToken);

        await _unitOfwork.CommitAsync(cancellationToken);

        return PermissionResponse.MapFromEntity(permissionCreated);
    }
}

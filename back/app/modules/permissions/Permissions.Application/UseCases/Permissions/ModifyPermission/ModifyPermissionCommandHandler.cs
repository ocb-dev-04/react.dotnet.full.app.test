using Permissions.Domain.Entities;
using Permissions.Domain.Abstractions;
using Shared.Common.Helper.ErrorsHandler;
using CQRS.MediatR.Helper.Abstractions.Messaging;
using Shared.Common.Helper.Abstractions.Providers;

namespace Permissions.Application.UseCases.Permissions;

internal sealed class ModifyPermissionCommandHandler
    : ICommandHandler<ModifyPermissionCommand>
{
    private readonly IUnitOfWork _unitOfwork;
    private readonly IEntitiesEventsManagementProvider _entitiesEventsManagementProvider;

    public ModifyPermissionCommandHandler(
        IUnitOfWork unitOfwork,
        IEntitiesEventsManagementProvider entitiesEventsManagementProvider)
    {
        ArgumentNullException.ThrowIfNull(unitOfwork, nameof(unitOfwork));
        ArgumentNullException.ThrowIfNull(entitiesEventsManagementProvider, nameof(entitiesEventsManagementProvider));

        _unitOfwork = unitOfwork;
        _entitiesEventsManagementProvider = entitiesEventsManagementProvider;
    }

    public async Task<Result> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
    {
        Result<Permission> permissionFound = await _unitOfwork.Permission.ByIdAsync(request.Id, cancellationToken);
        if (permissionFound.IsFailure)
            return Result.Failure<PermissionResponse>(permissionFound.Error);

        Result<PermissionType> permissionTypeFound = await _unitOfwork.PermissionType.ByIdAsync(permissionFound.Value.PermissionTypeId, cancellationToken);
        if (permissionTypeFound.IsFailure)
            return Result.Failure<PermissionResponse>(permissionTypeFound.Error);

        new PermissionBuilder(permissionFound.Value)
            .SetName(request.Body.EmployeeName)
            .SetLastName(request.Body.EmployeeLastName)
            .Build(_entitiesEventsManagementProvider);

        new PermissionTypeBuilder(permissionTypeFound.Value)
            .SetDescription(request.Body.Description)
            .Build(_entitiesEventsManagementProvider);

        await _unitOfwork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}

using Permissions.Domain.Entities;
using Permissions.Domain.Abstractions;
using Shared.Common.Helper.ErrorsHandler;
using CQRS.MediatR.Helper.Abstractions.Messaging;
using Permissions.Application.UseCases.CommonResponses;

namespace Permissions.Application.UseCases.Permissions;

internal sealed class GetPermissionsQueryHandler
    : IQueryHandler<GetPermissionsQuery, PaginatedCollection<PermissionResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPermissionsQueryHandler(IUnitOfWork unitOfWork)
    {
        ArgumentNullException.ThrowIfNull(unitOfWork, nameof(unitOfWork));

        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PaginatedCollection<PermissionResponse>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        (IReadOnlyCollection<Permission> Collection, int TotalItems, int TotalPages) collection = await _unitOfWork.Permission.CollectionAsync(request.PageNumber, cancellationToken);
        PaginatedCollection<PermissionResponse> mapped = PaginatedCollection<PermissionResponse>.Map(
            collection.Collection.Select(s => PermissionResponse.MapFromEntity(s)),
            collection.TotalItems,
            collection.TotalPages);

        return Result.Success(mapped);
    }
}

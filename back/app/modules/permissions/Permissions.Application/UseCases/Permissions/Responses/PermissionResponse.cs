using Permissions.Domain.Entities;

namespace Permissions.Application.UseCases.Permissions;

public sealed record PermissionResponse(
    int Id,
    string EmployeeName,
    string EmployeeLastName,
    int PermissionType,
    string Description,
    DateTimeOffset PermissionDateOnUtc)
{
    public static PermissionResponse MapFromEntity(Permission entity)
        => new(
            entity.Id,
            entity.EmployeeName.Value,
            entity.EmployeeLastName.Value,
            entity.PermissionType.Id,
            entity.PermissionType.Description.Value,
            entity.PermissionDateOnUtc);
}
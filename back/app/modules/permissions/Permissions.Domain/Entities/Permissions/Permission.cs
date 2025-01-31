using Value.Objects.Helper.Values.Primitives;

namespace Permissions.Domain.Entities;

public sealed partial class Permission
{
    public int Id { get; private set; }

    public StringObject EmployeeName { get; private set; }
    public StringObject EmployeeLastName { get; private set; }

    public int PermissionTypeId { get; private set; }
    public PermissionType PermissionType { get; private set; }

    public DateTimeOffset PermissionDateOnUtc { get; init; } = DateTimeOffset.UtcNow;

    internal Permission()
    {
        
    }
}

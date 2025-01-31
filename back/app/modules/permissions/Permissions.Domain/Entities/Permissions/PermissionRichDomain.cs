using Value.Objects.Helper.Values.Primitives;

namespace Permissions.Domain.Entities;

public sealed partial class Permission
{
    internal void SetName(string name)
        => EmployeeName = StringObject.Create(name);

    internal void SetLastName(string lastname)
        => EmployeeLastName = StringObject.Create(lastname);

    internal void SetType(PermissionType type)
    {
        PermissionType = type;
        PermissionTypeId = type.Id;
    }
}

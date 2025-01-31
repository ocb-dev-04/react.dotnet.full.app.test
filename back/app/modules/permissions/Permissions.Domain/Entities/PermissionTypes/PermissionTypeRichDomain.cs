using Value.Objects.Helper.Values.Primitives;

namespace Permissions.Domain.Entities;

public sealed partial class PermissionType
{
    internal void SetDescription(string description)
        => Description = StringObject.Create(description);
}

using Value.Objects.Helper.Values.Primitives;

namespace Permissions.Domain.Entities;

public sealed partial class PermissionType
{
    public int Id { get; private set; }
    public StringObject Description { get; private set; }

    internal PermissionType()
    {
        
    }
}

using Permissions.Domain.Abstractions.Builders;
using Shared.Common.Helper.Abstractions.Providers;

namespace Permissions.Domain.Entities;

public sealed class PermissionTypeBuilder
    : IPermissionTypeBuilder<PermissionTypeBuilder>
{
    private readonly PermissionType _instance;

    public PermissionTypeBuilder()
    {
        _instance = new();
    }

    public PermissionTypeBuilder(PermissionType currentInstance)
    {
        _instance = currentInstance;
    }

    public PermissionTypeBuilder SetDescription(string description)
    {
        _instance.SetDescription(description);
        return this;
    }

    public PermissionType Build(IEntitiesEventsManagementProvider entitiesEventsManagementProvider)
        => _instance;
}

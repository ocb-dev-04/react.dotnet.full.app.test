using Permissions.Domain.Abstractions.Builders;
using Permissions.Domain.Events;
using Shared.Common.Helper.Abstractions.Providers;

namespace Permissions.Domain.Entities;

public sealed class PermissionBuilder
    : IPermissionBuilder<PermissionBuilder>
{
    private readonly Permission _instance;

    public PermissionBuilder()
    {
        _instance = new();
    }

    public PermissionBuilder(Permission currentInstance)
    {
        _instance = currentInstance;
    }

    /// <inheritdoc/>
    public PermissionBuilder SetName(string name)
    {
        _instance.SetName(name);
        return this;
    }

    /// <inheritdoc/>
    public PermissionBuilder SetLastName(string lastName)
    {
        _instance.SetLastName(lastName);
        return this;
    }

    /// <inheritdoc/>
    public PermissionBuilder SetType(PermissionType type)
    {
        _instance.SetType(type);
        return this;
    }

    /// <inheritdoc/>
    public Permission Build(IEntitiesEventsManagementProvider entitiesEventsManagementProvider)
    {
        entitiesEventsManagementProvider.RaiseDomainEvent(new PermissionCreatedOrModifiedDomainEvent(_instance.Id));

        return _instance;
    }
}

using Shared.Common.Helper.Abstractions.Providers;

namespace Permissions.Domain.Abstractions.Builders;

internal interface IBuilder<out T>
{
    /// <summary>
    /// Return an <see cref="{T}"/> instance
    /// </summary>
    /// <returns></returns>
    T Build(IEntitiesEventsManagementProvider entitiesEventsManagementProvider);
}

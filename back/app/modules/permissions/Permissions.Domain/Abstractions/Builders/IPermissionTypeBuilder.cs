using Permissions.Domain.Entities;

namespace Permissions.Domain.Abstractions.Builders;

internal interface IPermissionTypeBuilder<out TBuilder>
    : IBuilder<PermissionType>
{
    /// <summary>
    /// Set description
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    TBuilder SetDescription(string description);
}

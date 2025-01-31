using Permissions.Domain.Entities;

namespace Permissions.Domain.Abstractions.Builders;

internal interface IPermissionBuilder<out TBuilder>
    : IBuilder<Permission>
{
    /// <summary>
    /// Set name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    TBuilder SetName(string name);

    /// <summary>
    /// Set lastname
    /// </summary>
    /// <param name="lastName"></param>
    /// <returns></returns>
    TBuilder SetLastName(string lastName);

    /// <summary>
    /// Set type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    TBuilder SetType(PermissionType type);
}

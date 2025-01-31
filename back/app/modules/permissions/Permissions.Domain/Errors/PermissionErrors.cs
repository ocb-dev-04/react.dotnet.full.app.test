using Shared.Common.Helper.ErrorsHandler;

namespace Permissions.Domain.Errors;

public sealed class PermissionErrors
{
    public static Error NotFound
        = Error.NotFound("permissionNotFound", "The permission was not found");
}

using Shared.Common.Helper.ErrorsHandler;

namespace Permissions.Domain.Errors;

public sealed class PermissionTypeErrors
{
    public static Error NotFound
        = Error.NotFound("permissionTypeNotFound", "The permission type was not found");
}
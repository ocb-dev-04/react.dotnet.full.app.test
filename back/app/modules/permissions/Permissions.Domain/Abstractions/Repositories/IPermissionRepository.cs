using Permissions.Domain.Entities;
using Permissions.Domain.Abstractions.Common;

namespace Permissions.Domain.Abstractions.Repositories;

public interface IPermissionRepository
    :  ISingleQueriesGenericRepository<Permission, int>,
        ICollectionQueriesGenericRepository<Permission>,
        ICreateGenericRepository<Permission>;

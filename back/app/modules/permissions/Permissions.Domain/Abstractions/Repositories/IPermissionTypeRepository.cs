using Permissions.Domain.Entities;
using Permissions.Domain.Abstractions.Common;

namespace Permissions.Domain.Abstractions.Repositories;

public interface IPermissionTypeRepository 
    : ISingleQueriesGenericRepository<PermissionType, int>, 
        ICreateGenericRepository<PermissionType>;
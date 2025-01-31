using Bogus;
using NSubstitute;
using Permissions.Domain.Errors;
using Permissions.Domain.Entities;
using Permissions.Domain.Abstractions;
using Shared.Common.Helper.ErrorsHandler;
using Shared.Common.Helper.Abstractions.Providers;

namespace Permissions.Application.Unit.Tests;

public abstract class BaseTestSharedConfiguration
{
    protected readonly Faker _faker;
    protected readonly IUnitOfWork _unitOfWorkMock;
    protected readonly IEntitiesEventsManagementProvider _eventsManagementProviderMock;

    protected readonly Permission _validPermission;
    protected readonly PermissionType _validPermissionType;

    protected const int ExampleId = 1;

    protected BaseTestSharedConfiguration()
    {
        _faker = new();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _eventsManagementProviderMock = Substitute.For<IEntitiesEventsManagementProvider>();

        _validPermissionType = new PermissionTypeBuilder()
            .SetDescription(_faker.Lorem.Paragraph(200))
            .Build(_eventsManagementProviderMock);

        _validPermission = new PermissionBuilder()
            .SetName(_faker.Person.FirstName)
            .SetLastName(_faker.Person.LastName)
            .SetType(_validPermissionType)
            .Build(_eventsManagementProviderMock);
    }

    public void Set_GetPermissionById_Success(int id)
        => _unitOfWorkMock.Permission.ByIdAsync(
            id,
            Arg.Any<CancellationToken>())
            .Returns(_validPermission);
    
    public void Set_GetPermissionById_NotFound()
        => _unitOfWorkMock.Permission.ByIdAsync(
            Arg.Any<int>(),
            Arg.Any<CancellationToken>())
            .Returns(
                Result.Failure<Permission>(
                    PermissionErrors.NotFound));

    public void Set_GetPermissionTypeById_Success(int id)
        => _unitOfWorkMock.PermissionType.ByIdAsync(
            id,
            Arg.Any<CancellationToken>())
            .Returns(_validPermissionType);

    public void Set_GetPermissionTypeById_NotFound()
        => _unitOfWorkMock.PermissionType.ByIdAsync(
            Arg.Any<int>(),
            Arg.Any<CancellationToken>())
            .Returns(
                Result.Failure<PermissionType>(
                    PermissionTypeErrors.NotFound));
}

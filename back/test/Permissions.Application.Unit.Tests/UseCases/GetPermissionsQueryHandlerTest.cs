using Bogus;
using FluentAssertions;
using NSubstitute;
using Permissions.Application.UseCases.CommonResponses;
using Permissions.Application.UseCases.Permissions;
using Permissions.Domain.Entities;
using Shared.Common.Helper.ErrorsHandler;
using System.Collections.ObjectModel;

namespace Permissions.Application.Unit.Tests.UseCases;

public sealed class GetPermissionsQueryHandlerTest
    : BaseTestSharedConfiguration
{
    private const int pageNumber = 1;
    private readonly GetPermissionsQuery _query;
    private readonly GetPermissionsQueryHandler _handler;

    public GetPermissionsQueryHandlerTest()
    {
        _query = new(pageNumber);
        _handler = new(_unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult()
    {
        // arrange
        Faker<Permission> collection = new Faker<Permission>()
            .CustomInstantiator(f =>
            {
                PermissionType? permissionType = new PermissionTypeBuilder()
                    .SetDescription(_faker.Lorem.Paragraph(200))
                    .Build(_eventsManagementProviderMock);

                return new PermissionBuilder()
                    .SetName(_faker.Person.FirstName)
                    .SetLastName(_faker.Person.LastName)
                    .SetType(permissionType)
                    .Build(_eventsManagementProviderMock);
            });
        (ReadOnlyCollection<Permission>, int, int) response = (collection.Generate(10).AsReadOnly(), 10, 1);

        _unitOfWorkMock.Permission.CollectionAsync(
                pageNumber,
                Arg.Any<CancellationToken>())
            .Returns(response);

        // act
        Result<PaginatedCollection<PermissionResponse>> result = await _handler.Handle(_query, default);

        // assert
        await _unitOfWorkMock.Permission.Received(1)
            .CollectionAsync(
                Arg.Is<int>(f => f.Equals(pageNumber)),
                default);

        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
}

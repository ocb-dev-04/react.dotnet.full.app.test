using FluentAssertions;
using NSubstitute;
using Permissions.Application.UseCases.Permissions;
using Permissions.Domain.Entities;
using Shared.Common.Helper.ErrorsHandler;

namespace Permissions.Application.Unit.Tests.UseCases;

public sealed class RequestPermissionCommandHandlerTest
    : BaseTestSharedConfiguration
{
    private readonly RequestPermissionCommand _command;
    private readonly RequestPermissionCommandHandler _handler;

    public RequestPermissionCommandHandlerTest()
    {
        _command = new(
            _faker.Person.FirstName,
            _faker.Person.LastName,
            _faker.Lorem.Paragraph(100));
        _handler = new(_unitOfWorkMock, _eventsManagementProviderMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult()
    {
        // arrange
        _unitOfWorkMock.PermissionType.CreateAsync(
                Arg.Any<PermissionType>(),
                Arg.Any<CancellationToken>())
            .Returns(_validPermissionType);

        _unitOfWorkMock.Permission.CreateAsync(
                Arg.Any<Permission>(),
                Arg.Any<CancellationToken>())
            .Returns(_validPermission);

        // act
        Result<PermissionResponse> result = await _handler.Handle(_command, default);

        // assert
        await _unitOfWorkMock.PermissionType.Received(1)
            .CreateAsync(Arg.Any<PermissionType>(), default);

        await _unitOfWorkMock.Permission.Received(1)
            .CreateAsync(Arg.Any<Permission>(), default);

        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
}

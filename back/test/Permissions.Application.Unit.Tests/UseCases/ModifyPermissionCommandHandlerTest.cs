using NSubstitute;
using FluentAssertions;
using Shared.Common.Helper.ErrorsHandler;
using Permissions.Application.UseCases.Permissions;
using System.Net;
using Permissions.Domain.Errors;

namespace Permissions.Application.Unit.Tests.UseCases;

public sealed class ModifyPermissionCommandHandlerTest
    : BaseTestSharedConfiguration
{
    private readonly ModifyPermissionCommand _command;
    private readonly ModifyPermissionCommandHandler _handler;

    public ModifyPermissionCommandHandlerTest()
    {
        ModifyPermissionRequest request = new(
            _faker.Person.FirstName, 
            _faker.Person.LastName, 
            _faker.Lorem.Paragraph(100));
        _command = new(ExampleId, request);
        _handler = new(_unitOfWorkMock, _eventsManagementProviderMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult()
    {
        // arrange
        Set_GetPermissionById_Success(ExampleId);
        Set_GetPermissionTypeById_Success(_validPermission.PermissionTypeId);

        // act
        Result result = await _handler.Handle(_command, default);

        // assert
        await _unitOfWorkMock.Permission.Received(1)
            .ByIdAsync(Arg.Is<int>(f
                => f.Equals(ExampleId)), default);

        await _unitOfWorkMock.PermissionType.Received(1)
            .ByIdAsync(Arg.Is<int>(f
                => f.Equals(_validPermission.PermissionTypeId)), default);

        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_PermmissionNotFound()
    {
        // arrange
        Set_GetPermissionById_NotFound();

        // act
        Result result = await _handler.Handle(_command, default);

        // assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(PermissionErrors.NotFound);
        result.Error.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_PermmissionTypeNotFound()
    {
        // arrange
        Set_GetPermissionById_Success(ExampleId);
        Set_GetPermissionTypeById_NotFound();

        // act
        Result result = await _handler.Handle(_command, default);

        // assert
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(PermissionTypeErrors.NotFound);
        result.Error.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }
}

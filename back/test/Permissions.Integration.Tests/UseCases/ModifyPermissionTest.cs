using FluentAssertions;
using CQRS.MediatR.Helper.Exceptions;
using Shared.Common.Helper.ErrorsHandler;
using Permissions.Application.UseCases.Permissions;

namespace Permissions.Integration.Tests.UseCases;

public sealed class ModifyPermissionTest : BaseIntegrationTest
{
    public ModifyPermissionTest(TestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ModifyPermission_ShouldSuccess_WhenCommandIsValid()
    {
        // arrange
        int permissionId = await CreatedPermisisonId();
        ModifyPermissionRequest modifyRequest = new (
            _faker.Person.FirstName,
            _faker.Person.LastName,
            _faker.Person.UserName);
        ModifyPermissionCommand modifyComand = new(permissionId, modifyRequest);

        // act
        Result modified = await _sender.Send(modifyComand);

        // assert
        modified.IsSuccess.Should().BeTrue();
        modified.IsFailure.Should().BeFalse();
    }

    [Fact]
    public async Task ModifyPermission_ShouldFailure_WhenNameIsEmpty()
    {
        // arrange
        int permissionId = await CreatedPermisisonId();
        ModifyPermissionRequest request = new(
            string.Empty,
            _faker.Person.LastName,
            _faker.Person.UserName);
        ModifyPermissionCommand comand = new(permissionId, request);

        // act
        Task Action() =>  _sender.Send(comand);

        // assert
        await Assert.ThrowsAsync<ValidationException>(Action);
    }

    [Fact]
    public async Task ModifyPermission_ShouldFailure_WhenLastNameIsEmpty()
    {
        // arrange
        int permissionId = await CreatedPermisisonId();
        ModifyPermissionRequest request = new(
            _faker.Person.FirstName,
            string.Empty,
            _faker.Person.UserName);
        ModifyPermissionCommand comand = new(permissionId, request);

        // act
        Task Action() => _sender.Send(comand);

        // assert
        await Assert.ThrowsAsync<ValidationException>(Action);
    }
    
    [Fact]
    public async Task ModifyPermission_ShouldFailure_WhenDescriptionIsEmpty()
    {
        // arrange
        int permissionId = await CreatedPermisisonId();
        ModifyPermissionRequest request = new(
            _faker.Person.FirstName,
            _faker.Person.LastName,
            string.Empty);
        ModifyPermissionCommand comand = new(permissionId, request);

        // act
        Task Action() => _sender.Send(comand);

        // assert
        await Assert.ThrowsAsync<ValidationException>(Action);
    }
}
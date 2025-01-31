using FluentAssertions;
using Permissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CQRS.MediatR.Helper.Exceptions;
using Shared.Common.Helper.ErrorsHandler;
using Permissions.Application.UseCases.Permissions;

namespace Permissions.Integration.Tests.UseCases;

public sealed class RequestPermissionTest : BaseIntegrationTest
{
    public RequestPermissionTest(TestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task RequestPermission_ShouldSuccess_WhenCommandIsValid()
    {
        // arrange
        RequestPermissionCommand? comand = new (
            _faker.Person.FirstName,
            _faker.Person.LastName,
            _faker.Person.UserName);

        // act
        Result<PermissionResponse> created = await _sender.Send(comand);

        // assert
        Permission? permission = await _appDbContext.Set<Permission>().FirstOrDefaultAsync(f => f.Id.Equals(created.Value.Id));

        permission.Should().NotBeNull();
        created.IsSuccess.Should().BeTrue();
        created.IsFailure.Should().BeFalse();
    }

    [Fact]
    public async Task RequestPermission_ShouldFailure_WhenNameIsEmpty()
    {
        // arrange
        RequestPermissionCommand? comand = new(
            string.Empty,
            _faker.Person.LastName,
            _faker.Person.UserName);

        // act
        Task Action() =>  _sender.Send(comand);

        // assert
        await Assert.ThrowsAsync<ValidationException>(Action);
    }

    [Fact]
    public async Task RequestPermission_ShouldFailure_WhenLastNameIsEmpty()
    {
        // arrange
        RequestPermissionCommand? comand = new(
            _faker.Person.FirstName,
            string.Empty,
            _faker.Person.UserName);

        // act
        Task Action() => _sender.Send(comand);

        // assert
        await Assert.ThrowsAsync<ValidationException>(Action);
    }
    
    [Fact]
    public async Task RequestPermission_ShouldFailure_WhenDescriptionIsEmpty()
    {
        // arrange
        RequestPermissionCommand? comand = new(
            _faker.Person.FirstName,
            _faker.Person.LastName,
            string.Empty);

        // act
        Task Action() => _sender.Send(comand);

        // assert
        await Assert.ThrowsAsync<ValidationException>(Action);
    }

}

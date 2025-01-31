using Bogus;
using MediatR;
using Persistence.Context;
using Shared.Common.Helper.ErrorsHandler;
using Microsoft.Extensions.DependencyInjection;
using Permissions.Application.UseCases.Permissions;

namespace Permissions.Integration.Tests;

public abstract class BaseIntegrationTest : IClassFixture<TestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly Faker _faker;

    protected readonly ISender _sender;
    protected readonly AppDbContext _appDbContext;

    private int? permissionId;

    protected BaseIntegrationTest(TestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        _faker = new();

        _sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        _appDbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }

    protected async Task<int> CreatedPermisisonId()
        => permissionId ??= await CreateNewPermission();

    private async Task<int> CreateNewPermission()
    {
        if (permissionId is not null) return (int)permissionId;

        RequestPermissionCommand? createCommand = new(
            _faker.Person.FirstName,
            _faker.Person.LastName,
            _faker.Person.UserName);
        Result<PermissionResponse> created = await _sender.Send(createCommand);
        return created.Value.Id;
    }
}

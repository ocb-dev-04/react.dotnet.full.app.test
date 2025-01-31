using FluentAssertions;
using Shared.Common.Helper.ErrorsHandler;
using Permissions.Application.UseCases.Permissions;
using Permissions.Application.UseCases.CommonResponses;

namespace Permissions.Integration.Tests.UseCases;

public sealed class GetPermissionsTest : BaseIntegrationTest
{
    public GetPermissionsTest(TestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetPermissions_ShouldSuccess_WhenQueryIsValid()
    {
        // arrange
        GetPermissionsQuery query = new GetPermissionsQuery(1);

        // act
       Result<PaginatedCollection<PermissionResponse>> response = await _sender.Send(query);

        // assert
        response.IsSuccess.Should().BeTrue();
        response.IsFailure.Should().BeFalse();
    }
}

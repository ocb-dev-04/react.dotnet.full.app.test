using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Presentation.Controllers.Base;
using Shared.Common.Helper.Extensions;
using Shared.Common.Helper.ErrorsHandler;
using System.ComponentModel.DataAnnotations;
using Permissions.Application.UseCases.Permissions;
using Permissions.Application.UseCases.CommonResponses;

namespace Presentation.Controllers;

[ApiController]
[Route("api/permissions")]
[Produces("application/json")]
[Consumes("application/json")]
public sealed class PermissionsController : BaseController
{
    public PermissionsController(ISender sender) : base(sender)
    {
    }

    #region Queries

    /// <summary>
    /// Get paginated permission collection
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedCollection<PermissionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBySelf(
        [FromQuery, Required] int pageNumber, 
        CancellationToken cancellationToken)
    {
        GetPermissionsQuery query = new(pageNumber);
        Result<PaginatedCollection<PermissionResponse>>? response = await _sender.Send(query, cancellationToken);

        return response.Match(Ok, HandleErrorResults);
    }

    #endregion

    #region Commands

    /// <summary>
    /// Create a new permission
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(PermissionResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(
        [FromBody] RequestPermissionCommand command, 
        CancellationToken cancellationToken)
    {
        Result<PermissionResponse> response = await _sender.Send(command, cancellationToken);

        return response.Match(
                success: data => Created(string.Empty, data),
                error: HandleErrorResults);
    }

    /// <summary>
    /// Update permission
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        [FromRoute, Required] int id, 
        [FromBody] ModifyPermissionRequest request, 
        CancellationToken cancellationToken)
    {
        ModifyPermissionCommand command = new(id, request);
        Result response = await _sender.Send(command, cancellationToken);

        return response.Match(NoContent, HandleErrorResults);
    }

    #endregion
}

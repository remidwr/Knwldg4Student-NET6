using Application.Features.RoleFeatures.Queries.GetRoles;

using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers
{
    public class RolesController : ApiController
    {
        /// <summary>
        /// Get all roles from Auth0.
        /// </summary>
        /// <response code="200">Roles returned</response>
        [HttpGet()]
        [Authorize(Policy = "view:roles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RolesVm))]
        public async Task<ActionResult<RolesVm>> GetRoles()
        {
            return await Mediator.Send(new GetRolesQuery());
        }
    }
}
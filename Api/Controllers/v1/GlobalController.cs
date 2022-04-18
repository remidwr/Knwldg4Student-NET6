using Application.Features.NumbersApiFeatures.Queries.GetDateFact;

using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class GlobalController : ApiController
    {
        [Authorize]
        [HttpGet("GetDateFact")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DateFactDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DateFactDto>> GetDateFact()
        {
            return await Mediator.Send(new GetDateFactQuery());
        }
    }
}
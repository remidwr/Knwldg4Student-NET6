using Application.Features.NumbersApiFeatures.Queries.GetDateFact;

using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers.v2
{
    [ApiVersion("2.0")]
    public class GlobalController : ApiController
    {
        [Authorize]
        [HttpGet("GetDateFact")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DateFactDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DateFactDto>> GetDateFactV2()
        {
            return await Mediator.Send(new GetDateFactQuery());
        }
    }
}
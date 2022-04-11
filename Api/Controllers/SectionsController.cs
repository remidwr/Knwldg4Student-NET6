using Application.Features.SectionFeatures.Queries.GetSectionById;

using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ApiController
    {
        [HttpGet]
        //[Authorize("read:messages")]
        public async Task<ActionResult<SectionsVm>> Get()
        {
            return await Mediator.Send(new GetSectionsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SectionDetailedDto>> GetById([FromRoute] int id)
        {
            return await Mediator.Send(new GetSectionByIdQuery { Id = id });
        }
    }
}
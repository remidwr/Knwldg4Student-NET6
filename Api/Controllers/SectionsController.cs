namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ApiController
    {
        [HttpGet]
        //[Authorize("read:messages")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SectionsVm))]
        public async Task<ActionResult<SectionsVm>> Get()
        {
            return await Mediator.Send(new GetSectionsQuery());
        }

        /// <summary>
        /// Get a section by id.
        /// </summary>
        /// <param name="id">Section id</param>
        /// <response code="200">Section returned</response>
        /// <response code="404">The section is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SectionDetailedDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SectionDetailedDto>> GetById([FromRoute] int id)
        {
            return await Mediator.Send(new GetSectionByIdQuery { Id = id });
        }
    }
}
namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ApiController
    {
        [HttpGet("FromStudent/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MeetingsVm))]
        public async Task<ActionResult<MeetingsVm>> GetMeetingsFromStudentId([FromRoute] int id)
        {
            return await Mediator.Send(new GetMeetingsFromStudentIdQuery() { Id = id });
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MeetingDetailedDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MeetingDetailedDto>> GetById([FromRoute] int id)
        {
            return await Mediator.Send(new GetMeetingByIdQuery() { Id = id });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create([FromBody] CreateMeetingCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
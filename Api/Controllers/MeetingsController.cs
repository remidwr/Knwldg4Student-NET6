namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ApiController
    {
        [HttpGet("FromStudent/{id:int}")]
        public async Task<ActionResult<MeetingsVm>> GetMeetingsFromStudentId([FromRoute] int id)
        {
            return await Mediator.Send(new GetMeetingsFromStudentIdQuery() { Id = id });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MeetingDetailedDto>> GetById([FromRoute] int id)
        {
            return await Mediator.Send(new GetMeetingByIdQuery() { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateMeetingCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
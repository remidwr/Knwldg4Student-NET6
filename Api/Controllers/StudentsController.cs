namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<StudentsVm>> Get()
        {
            return await Mediator.Send(new GetStudentsQuery());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudentDetailedDto>> GetById([FromRoute] int id)
        {
            return await Mediator.Send(new GetStudentByIdQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateStudentCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("Rating")]
        public async Task<ActionResult<int>> RateStudent(RateStudentCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, UpdateStudentCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
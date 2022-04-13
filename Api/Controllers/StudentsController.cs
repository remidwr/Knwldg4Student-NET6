namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentsVm))]
        public async Task<ActionResult<StudentsVm>> Get()
        {
            return await Mediator.Send(new GetStudentsQuery());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentDetailedDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDetailedDto>> GetById([FromRoute] int id)
        {
            return await Mediator.Send(new GetStudentByIdQuery { Id = id });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create(CreateStudentCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("Rating")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> RateStudent(RateStudentCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update([FromRoute] int id, UpdateStudentCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
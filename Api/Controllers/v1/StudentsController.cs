using Application.Features.StudentFeatures.Commands.AddUnavailableDay;
using Application.Features.StudentFeatures.Commands.AssignRolesToStudent;

using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class StudentsController : ApiController
    {
        /// <summary>
        /// Get all students.
        /// </summary>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentsVm))]
        public async Task<ActionResult<StudentsVm>> Get()
        {
            return await Mediator.Send(new GetStudentsQuery());
        }

        /// <summary>
        /// Find a student.
        /// </summary>
        /// <param name="id">Student's id</param>
        /// <response code="200">Student returned</response>
        /// <response code="404">Student not found</response>
        [HttpGet("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentDetailedDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDetailedDto>> GetById([FromRoute] int id)
        {
            return await Mediator.Send(new GetStudentByIdQuery(id));
        }

        /// <summary>
        /// Add a new student.
        /// </summary>
        /// <param name="command">Student's inputs</param>
        /// <response code="201">Student successfully added</response>
        /// <response code="400">Invalid request</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create(CreateStudentCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Assign roles to student.
        /// </summary>
        /// <param name="id">Student id</param>
        /// <param name="command">Student and roles inputs</param>
        /// <response code="204">Roles successfully assigned to student</response>
        /// <response code="400">Invalid request</response>
        [HttpPost("{id}/roles")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Unit>> AssignRolesToStudent([FromRoute] string id, AssignRolesToStudentCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Give a rating to a student.
        /// </summary>
        /// <param name="command">Rating inputs</param>
        /// <response code="201">Rating successfully added</response>
        /// <response code="404">Student or joint meeting not found</response>
        /// <response code="400">Invalid request</response>
        [HttpPost("Rating")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> RateStudent(RateStudentCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Edit student informations.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <response code="204">Student's informations successfully edited</response>
        /// <response code="404">Student not found</response>
        /// <response code="400">Invalid request</response>
        [HttpPut("{id:int}")]
        [Authorize]
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

        [HttpPost("{id:int}/DayOff")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> AddUnavailableDay([FromRoute] int id, CreateUnavailableDayCommand command)
        {
            return Ok();
        }
    }
}
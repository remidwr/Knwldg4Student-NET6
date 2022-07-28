using Application.Features.StudentFeatures.Commands.AddUnavailableDay;
using Application.Features.StudentFeatures.Commands.AssignRolesToStudent;
using Application.Features.StudentFeatures.Queries.GetRolesByUserId;

using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers
{
    public class StudentsController : ApiController
    {
        /// <summary>
        /// Get all students.
        /// </summary>
        [HttpGet]
        [Authorize(Policy = "view:student")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentsVm))]
        public async Task<ViewResult> Get()
        {
            var students = await Mediator.Send(new GetStudentsQuery());

            return View(students);
        }

        /// <summary>
        /// Find a student.
        /// </summary>
        /// <param name="id">Student's id</param>
        /// <response code="200">Student returned</response>
        /// <response code="404">Student not found</response>
        [HttpGet("{id}")]
        [Authorize(Policy = "view:student")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentDetailedDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDetailedDto>> GetById([FromRoute] string id)
        {
            return await Mediator.Send(new GetStudentByIdQuery(id));
        }

        /// <summary>
        /// Get student's roles
        /// </summary>
        /// <param name="id">Student's id</param>
        /// <response code="200">Student'roles returned</response>
        [HttpGet("{id}/role")]
        [Authorize(Policy = "view:student")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersRoleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsersRoleDto>> GetRoleByStudentId([FromRoute] string id)
        {
            return await Mediator.Send(new GetRoleByUserIdQuery(id));
        }

        /// <summary>
        /// Add a new student.
        /// </summary>
        /// <param name="command">Student's inputs</param>
        /// <response code="201">Student successfully added</response>
        /// <response code="400">Invalid request</response>
        [HttpPost]
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
        [Authorize(Policy = "create:assign-role")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Unit>> AssignRolesToStudent([FromRoute] string id, AssignRolesToStudentCommand command)
        {
            if (id != command.StudentExternalId)
                return BadRequest();

            return await Mediator.Send(command);
        }

        /// <summary>
        /// Give a rating to a student.
        /// </summary>
        /// <param name="id">Student id to rate</param>
        /// <param name="command">Rating inputs</param>
        /// <response code="201">Rating successfully added</response>
        /// <response code="404">Student or joint meeting not found</response>
        /// <response code="400">Invalid request</response>
        [HttpPost("{id}/rate")]
        [Authorize(Policy = "create:rate-student")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> RateStudent([FromRoute] string id, RateStudentCommand command)
        {
            //TODO return badRequest when ids are not the same

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
        [Authorize(Policy = "update:student")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Unit>> Update([FromRoute] int id, UpdateStudentCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return await Mediator.Send(command);
        }

        /// <summary>
        /// Add unavailable days
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id:int}/DayOff")]
        [Authorize(Policy = "create:student-dayoff")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> AddUnavailableDay([FromRoute] int id, CreateUnavailableDayCommand command)
        {
            return Ok();
        }
    }
}
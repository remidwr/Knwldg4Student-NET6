using System.ComponentModel.DataAnnotations;

using Application.Features.UdemyFeatures.Queries.GetCourseList;

namespace WebUI.Controllers
{
    public class UdemysController : ApiController
    {
        /// <summary>
        /// Get all courses from Udemy.
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="search">Search keyword to filter courses from Udemy</param>
        /// <param name="ordering">Courses from Udemy ordered by (default: relevant)</param>
        /// <response code="200">Courses from Udemy returned</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        //[Authorize(Policy = "view")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseListDto))]
        public async Task<ActionResult<CourseListDto>> GetMeetingsFromStudentId([Required][FromQuery] int pageNumber, [Required][FromQuery] int pageSize, [FromQuery] string search, [FromQuery] string ordering)
        {
            return await Mediator.Send(new GetCourseListQuery(pageNumber, pageSize, search, ordering));
        }
    }
}
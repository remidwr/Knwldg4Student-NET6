﻿using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MeetingsController : ApiController
    {
        /// <summary>
        /// Get all meetings from a student.
        /// </summary>
        /// <param name="id">Student id</param>
        /// <response code="200">Meetings returned</response>
        [HttpGet]
        [Authorize(Policy = "view:meetings")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MeetingsVm))]
        public async Task<ActionResult<MeetingsVm>> GetMeetingsFromStudentId()
        {
            return await Mediator.Send(new GetMeetingsQuery());
        }

        /// <summary>
        /// Find a meeting.
        /// </summary>
        /// <param name="id">Meeting id</param>
        /// <response code="200">Meeting returned</response>
        /// <response code="404">Meeting not found</response>
        [HttpGet("{id:int}")]
        [Authorize(Policy = "view:meetings")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MeetingDetailedDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MeetingDetailedDto>> GetById([FromRoute] int id)
        {
            return await Mediator.Send(new GetMeetingByIdQuery(id));
        }

        /// <summary>
        /// Add a new meeting.
        /// </summary>
        /// <param name="command">Meeting inputs</param>
        /// <response code="201">Meeting added</response>
        /// <response code="400">Invalid request</response>
        [HttpPost]
        [Authorize(Policy = "create:meetings")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create([FromBody] CreateMeetingCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
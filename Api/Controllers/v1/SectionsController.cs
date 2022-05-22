﻿using Api.Infrastructure.Services;

namespace Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SectionsController : ApiController
    {
        private readonly IIdentityService _identityService;

        public SectionsController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Get all sections
        /// </summary>
        /// <response code="200">Sections returned</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SectionsVm))]
        public async Task<ActionResult<SectionsVm>> Get()
        {
            return await Mediator.Send(new GetSectionsQuery());
        }

        /// <summary>
        /// Find a section with courses
        /// </summary>
        /// <param name="id">Section id</param>
        /// <response code="200">Section with courses returned</response>
        /// <response code="404">Section not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SectionDetailedDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SectionDetailedDto>> GetById([FromRoute] int id)
        {
            return await Mediator.Send(new GetSectionByIdQuery(id));
        }
    }
}
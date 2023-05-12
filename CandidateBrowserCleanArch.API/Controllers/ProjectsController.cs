using CandidateBrowserCleanArch.API.Configurations;
using CandidateBrowserCleanArch.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CandidateBrowserCleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiVersion(ApiVersionNumber.V1_0)]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = CustomRoleClaims.CandidateRead)]
        public async Task<ActionResult<ServiceReponse<IEnumerable<ReadProjectDto>>>>GetAllActivePrjects()
        {
            var response = await _mediator.Send(new GetActiveProjectsRequest());
            return Ok(response);
        }
    }
}

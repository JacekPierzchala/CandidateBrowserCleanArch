using CandidateBrowserCleanArch.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CandidateBrowserCleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateProjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CandidateProjectController> _logger;

        public CandidateProjectController(IMediator mediator,
                ILogger<CandidateProjectController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("candidate/{candidateId}")]
        [Authorize(Policy = CustomRoleClaims.CandidateRead)]
        public async Task<ActionResult<ServiceReponse<IEnumerable<CandidateProjectDto>>>> GetAllProjectsByCandidate(int candidateId)
        {
            var response = await _mediator.Send(new GetCandidateProjectsByCandidateRequest { CandidateId = candidateId });
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = CustomRoleClaims.CandidateRead)]
        public async Task<ActionResult<ServiceReponse<CandidateProjectDto>>> GetCandidateProjectDetails(int id)
        {
            var response = await _mediator.Send(new GetCandidateProjectsDetailsRequest { Id = id });
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = CustomRoleClaims.CandidateUpdate)]
        public async Task<ActionResult<ServiceReponse<CandidateProjectDto>>> AddCandidateProject
        ([FromBody] CandidateProjectAddDto candidateProjectAddDto)
        {
            var response = await _mediator.Send(new AddCandidateProjectCommand { CandidateProjectAddDto = candidateProjectAddDto });
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = CustomRoleClaims.CandidateUpdate)]
        public async Task<ActionResult<ServiceReponse<CandidateProjectDto>>> UpdateCandidateProject
                    (int id, [FromBody] CandidateProjectUpdateDto candidateProjectUpdateDto)
        {
            var response = await _mediator.Send(
                            new UpdateCandidateProjectCommand
                            {
                                Id = id,
                                 CandidateProjectUpdateDto = candidateProjectUpdateDto
                            });
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = CustomRoleClaims.CandidateUpdate)]
        public async Task<ActionResult<ServiceReponse<bool>>> DeleteCandidateProject(int id)
        {
            var response = await _mediator.Send(new DeleteCandidateProjectCommand { Id = id });
            return Ok(response);
        }
    }
}

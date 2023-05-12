using CandidateBrowserCleanArch.API.Configurations;
using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Applicationl;
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
    public class CandidatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CandidatesController> _logger;

        public CandidatesController(IMediator mediator, ILogger<CandidatesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = CustomRoleClaims.CandidateRead)]

        public async Task<ActionResult<PagedResultResponse<CandidateListDto>>> GetAllCandidates([FromQuery] CandidateQueryParameters queryParameters)
        {
            var response = await _mediator.Send(new GetActiveCandidatesListRequest { QueryParameters = queryParameters });
            return Ok(response);
        }



        [HttpGet("{id}")]
        [Authorize(Policy = CustomRoleClaims.CandidateRead)]
        public async Task<ActionResult<ServiceReponse<CandidateDetailsDto>>> GetCandidateDetails(int id)
        {
            var response = await _mediator.Send(new GetCandidateDetailsRequest { CandidateId = id });
            return Ok(response);         
        }

        [HttpPost]
        [Authorize(Policy = CustomRoleClaims.CandidateCreate)]
        public async Task<ActionResult<ServiceReponse<CandidateDetailsDto>>> CreateCandidate([FromBody] CandidateCreateDto createCandidate)
        {
            var response = await _mediator.Send(new AddCandidateCommand { CreateCandidateDto=createCandidate });
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = CustomRoleClaims.CandidateUpdate)]
        public async Task<ActionResult<ServiceReponse<CandidateDetailsDto>>> UpdateCandidate(int id,[FromBody] CandidateUpdateDto candidateUpdate)
        {
            var response = await _mediator.Send(new UpdateCandidateCommand { CandidateUpdate = candidateUpdate, Id=id });
            return Ok(response);
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = CustomRoleClaims.CandidateDelete)]
        public async Task<ActionResult<BaseResponse>> DeleteCandidate(int id)
        {
            var response = await _mediator.Send(new DeleteCandidateCommand { CandidateId = id });
            return Ok(response);
        }
    }
}

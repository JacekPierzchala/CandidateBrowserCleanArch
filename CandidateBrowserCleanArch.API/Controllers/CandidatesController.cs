using CandidateBrowserCleanArch.Application;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CandidateBrowserCleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PagedResultResponse<CandidateListDto>>> GetAllCandidates([FromQuery] CandidateQueryParameters queryParameters)
        {
            var response = await _mediator.Send(new GetActiveCandidatesListRequest { QueryParameters = queryParameters });
            return Ok(response);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ServiceReponse<CandidateDetailsDto>>> GetCandidateDetails(int id)
        {
            var response = await _mediator.Send(new GetCandidateDetailsRequest { CandidateId = id });
            return Ok(response);         
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ServiceReponse<CandidateDetailsDto>>> CreateCandidate([FromBody] CreateCandidateDto createCandidate)
        {
            var response = await _mediator.Send(new AddCandidateCommand { CreateCandidateDto=createCandidate });
            return Ok(response);
        }
    }
}

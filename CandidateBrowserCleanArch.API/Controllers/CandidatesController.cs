using CandidateBrowserCleanArch.Application.DTOs;
using CandidateBrowserCleanArch.Application.DTOs.Candidate;
using CandidateBrowserCleanArch.Application.Feature.Candidate.Queries;
using CandidateBrowserCleanArch.Application.Responses;
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
            var response = await _mediator.Send(new GetActiveCandidatesListRequest { QueryParameters=queryParameters });
            if(response.Success)
            {
                return Ok(response);
            }
            else
            {
                _logger.LogError(response.Message);
                return StatusCode(500);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ServiceReponse<CandidateDetailsDto>>>GetCandidateDetails(int id)
        {
            var response = await _mediator.Send(new GetCandidateDetailsRequest { CandidateId = id });
            if(response.Data!=null) 
            {
                return Ok(response);
            }
            else if(response.Data == null && response.Success)
            {
                return NotFound();
            }
            else
            {
                _logger.LogError(response.Message);
                return StatusCode(500);
            }
        }
    }
}

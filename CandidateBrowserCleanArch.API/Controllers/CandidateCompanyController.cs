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
    [ApiVersion(ApiVersionNumber.V1_0)]
    public class CandidateCompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CandidateCompanyController> _logger;

        public CandidateCompanyController(IMediator mediator,
            ILogger<CandidateCompanyController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet("candidate/{candidateId}")]
        [Authorize(Policy = CustomRoleClaims.CandidateRead)]
        public async Task<ActionResult<ServiceReponse<IEnumerable<CandidateCompanyDto>>>> GetAllCompaniesByCandidate(int candidateId)
        {
            var response = await _mediator.Send(new GetCandidateCompaniesByCandidateRequest { CandidateId= candidateId });
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = CustomRoleClaims.CandidateRead)]
        public async Task<ActionResult<ServiceReponse<CandidateCompanyDto>>> GetCandidateCompanyDetails(int id)
        {
            var response = await _mediator.Send(new GetCandidateCompanyDetailsRequest { Id = id });
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = CustomRoleClaims.CandidateUpdate)]
        public async Task<ActionResult<ServiceReponse<CandidateCompanyDto>>> AddCandidateCompany
            ([FromBody] CandidateCompanyAddDto candidateCompanyAddDto)
        {
            var response = await _mediator.Send(new AddCandidateCompanyCommand { CandidateCompanyAddDto = candidateCompanyAddDto });
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = CustomRoleClaims.CandidateUpdate)]
        public async Task<ActionResult<ServiceReponse<CandidateCompanyDto>>> UpdateCandidateCompany
            (int id, [FromBody] CandidateCompanyUpdateDto candidateCompanyUpdateDto)
        {
            var response = await _mediator.Send(
                            new UpdateCandidateCompanyCommand
                            {
                                Id = id,
                                CandidateCompanyUpdateDto = candidateCompanyUpdateDto
                            });
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = CustomRoleClaims.CandidateUpdate)]
        public async Task<ActionResult<ServiceReponse<bool>>> DeleteCandidateCompany(int id)
        {
            var response = await _mediator.Send(new DeleteCandidateCompanyCommand {  Id=id });
            return Ok(response);
        }
    }
}

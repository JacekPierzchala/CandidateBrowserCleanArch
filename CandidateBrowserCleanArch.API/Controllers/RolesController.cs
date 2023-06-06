using CandidateBrowserCleanArch.API.Configurations;
using CandidateBrowserCleanArch.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CandidateBrowserCleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion(ApiVersionNumber.V1_0)]
    [Authorize(Policy =CustomRoleClaims.UserUpdate)]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceReponse<IEnumerable<RoleDto>>>>GetRoles()
        {
            var response = await _mediator.Send(new GetRolesRequest());
            return Ok(response);
        }
    }
}

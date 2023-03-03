using CandidateBrowserCleanArch.API.Configurations;
using CandidateBrowserCleanArch.Application;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CandidateBrowserCleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion(ApiVersionNumber.V1_0)]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest authRequest)
        {
            var response = await _mediator.Send(new LoginCommand {  AuthRequest = authRequest });
            return Ok(response);
        }
        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<RegistrationResponse>>Register(RegistrationRequest registrationRequest)
        {
            var response=await _mediator.Send(new RegisterCommand { RegistrationRequest= registrationRequest });
            return Ok(response); 
        }
    }
}

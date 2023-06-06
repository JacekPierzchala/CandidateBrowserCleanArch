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
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy =CustomRoleClaims.UserUpdate)]
        [HttpGet]
        public async Task<ActionResult<ServiceReponse<IEnumerable<ReadUserListDto>>>>GetUsers()
        {
            var response = await _mediator.Send(new GetUsersRequest());
            return Ok(response);
        }

        [Authorize(Policy = CustomRoleClaims.UserUpdate)]
        [HttpGet("byId/{userId}")]
        public async Task<ActionResult<ServiceReponse<ReadUserDetailsDto>>> GetUserDetails(string userId)
        {
            var response = await _mediator.Send(new GetUserDetailsRequest {  UserId=userId});
            return Ok(response);
        }

        [Authorize(Policy = CustomRoleClaims.UserUpdate)]
        [HttpPut("{userId}")]
        public async Task<ActionResult<ServiceReponse<bool>>> UpdateUser(string userId, [FromBody] UpdateUserDto updateUser)
        {
            var response = await _mediator.Send(new UpdateUserRequest { UserId = userId, UpdateUser=updateUser });
            return Ok(response);
        }
    }
}

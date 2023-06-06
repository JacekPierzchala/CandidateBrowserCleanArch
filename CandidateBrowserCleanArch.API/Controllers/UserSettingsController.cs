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
    [Authorize]
    public class UserSettingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSettingsController(IMediator mediator,
            IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("themes")]
        public async Task<ActionResult<ServiceReponse<IEnumerable<ConfigThemeDto>>>> GetThemes()
        {
            var response = await _mediator.Send(new GetThemesRequest());
            return Ok(response);
        }

        [HttpGet("settings/{userId}")]
        public async Task<ActionResult<ServiceReponse<UserSettingsDto>>> GetUserSettings(string userId)
        {
            var response = await _mediator.Send(new GetUserSettingsRequest() {  UserId=userId});
            return Ok(response);
        }

        [HttpPut("settings/{userId}")]
        public async Task<ActionResult<ServiceReponse<bool>>> UpdateUserSettings(string userId, [FromBody] UserSettingsDto userSettings)
        {
            if(_httpContextAccessor.HttpContext.User.Claims.Any(c=>c.Value==CustomRoleClaims.UserUpdate)||
               _httpContextAccessor.HttpContext.User.Claims.Any(c=>c.Type==CustomClaimTypes.Uid && c.Value== userSettings.UserId))
            {
                var response = await _mediator.Send(new UpdateUserSettingsRequest() { UserId = userId,  UserSettingsDto=userSettings });
                return Ok(response);
            }
            return Unauthorized();
        }
    }
}

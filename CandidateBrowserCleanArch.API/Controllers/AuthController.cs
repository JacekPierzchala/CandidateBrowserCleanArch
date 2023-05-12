using CandidateBrowserCleanArch.API.Configurations;
using CandidateBrowserCleanArch.Application;
using IdentityModel.Client;
using Mailjet.Client.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Encodings.Web;
using System.Web;

namespace CandidateBrowserCleanArch.API.Controllers;


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
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest authRequest)
    {
        var response = await _mediator.Send(new LoginCommand {  AuthRequest = authRequest });
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>>Register(RegistrationRequest registrationRequest)
    {

        var requestUrl = new Uri($"{Request.Scheme}://{Request.Host}/api/Auth/confirmEmail?");
        var response=await _mediator.Send(new RegisterCommand { RegistrationRequest= registrationRequest, RequestUrl=requestUrl.AbsoluteUri });
        return Ok(response); 
    }

    [HttpGet("confirmEmail")]
    public async Task<ActionResult> ConfirmEmail(string userId,string token, string redirectUrl)
    {
        var request = new ConfirmEmailRequest
        {
            UserId = userId,
            Token =  token,
        };
        var response= await _mediator.Send(new ConfirmEmailCommand { ConfirmEmailRequest=request});
        return Redirect(redirectUrl);
    }


    [HttpPost("confirmEmail-repeat")]
    public async Task<ActionResult<ServiceReponse<bool>>> ConfirmEmailRepeat(ConfirmEmailRepeatRequest request)
    {
        var requestUrl = new Uri($"{Request.Scheme}://{Request.Host}/api/Auth/confirmEmail?");
        var response = await _mediator.Send(new ConfirmEmailRepeatCommand {  ConfirmEmailRepeatRequest = request, RequestUrl=requestUrl.AbsoluteUri });
        return Ok(response);
    }

    [HttpPost("forgotPassword")]
    public async Task<ActionResult<ServiceReponse<bool>>> ForgotPassword([FromBody]ForgotPasswordRequest request)
    {
        var response = await _mediator.Send(new ForgotPasswordCommand { ForgotPasswordRequest = request });
        return Ok(response);
    }


    [HttpPost("resetPassword")]
    public async Task<ActionResult<ServiceReponse<bool>>> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var response = await _mediator.Send(new ResetPasswordCommand {  ResetPasswordRequest = request });
        return Ok(response);
    }

    [HttpPost("login-google")]
    public async Task<ActionResult<AuthResponse>> LoginGoogle([FromHeader] string authCode, string redirectUrl)
    {
        var response = await _mediator.Send(new GoogleAuthCommand { AuthCode= authCode, RedirectUrl= redirectUrl });       
        return Ok(response);
    }

    [HttpGet("getGoogleRedirectUrl")]
    public async Task<ActionResult<ServiceReponse<string>>> GetGoogleRedirectUrl(string redirectUrl )
    {
        var response = await _mediator.Send(new GetGoogleUrlQuery { RedirectUrl=redirectUrl});
        return Ok(response);

    }

}

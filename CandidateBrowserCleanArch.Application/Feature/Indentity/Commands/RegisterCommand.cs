using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CandidateBrowserCleanArch.Application;

public class RegisterCommand:IRequest<RegistrationResponse> 
{
    public RegistrationRequest RegistrationRequest { get; set; }
    public string RequestUrl { get; set; }
}

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegistrationResponse>
{
    private readonly IAuthService _authService;
    private readonly IEmailSenderService _emailSender;

    public RegisterCommandHandler(IAuthService authService, IEmailSenderService emailSender)
    {
        _authService = authService;
        _emailSender = emailSender;
    }
    public async Task<RegistrationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var validator = new RegistrationRequestValidator();
        var validationResult = await validator.ValidateAsync(request.RegistrationRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }

        var response =await _authService.Register(request.RegistrationRequest);
        if (!response.Success)
        {
            throw new RegistrationException(response.Errors);
        }
      
        var requestUrl = $"{request.RequestUrl}userId={HttpUtility.UrlEncode(response.EncryptedUserId)}&token={HttpUtility.UrlEncode(response.ValidToken)}&redirectUrl={request.RegistrationRequest.ReturnUrl}";       
        var message =  "<p>Welcome to Candidates Browser </p> <p>Please confirm you email account by clicking this link <br> <a href=\""+ requestUrl + "\" >Click here </a> </p>";
        var emailRequest= await _emailSender.SendEmailAsync(request.RegistrationRequest.Email, "Candidates Browser registration", message);
        if(!emailRequest)
        {
            throw new EmailSenderException("Something went wrong with email confirmation. Please resend email");
        }
        return response;
    }
}


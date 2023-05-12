using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CandidateBrowserCleanArch.Application;

public class ForgotPasswordCommand:IRequest<ServiceReponse<bool>>
{
    public ForgotPasswordRequest ForgotPasswordRequest { get; set; }
}
public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ServiceReponse<bool>>
{
    private readonly IAuthService _authService;
    private readonly IEmailSenderService _emailSender;

    public ForgotPasswordCommandHandler(IAuthService authService, IEmailSenderService emailSender)
    {
        _authService = authService;
        _emailSender = emailSender;
    }
    public async Task<ServiceReponse<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var response= new ServiceReponse<bool>();
        var validator = new ForgotPasswordRequestValidator();
        var validationResult = await validator.ValidateAsync(request.ForgotPasswordRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }

        var result = await _authService.ForgotPassword(request.ForgotPasswordRequest);
        if (!result.Success)
        {
            throw new BadRequestException(result.Message);
        }

        var requestUrl = $"{request.ForgotPasswordRequest.ReturnUrl}token={HttpUtility.UrlEncode(result.ValidToken)}&userid={HttpUtility.UrlEncode(result.EncryptedUserId)}";
        var message = "<p>Welcome to Candidates Browser </p> <p>Please reset you  account password by clicking this link <br> <a href=\"" + requestUrl + "\" >Click here </a> </p>";
        var emailRequest = await _emailSender.SendEmailAsync(request.ForgotPasswordRequest.Email, "Candidates Browser password reset", message);
        if (!emailRequest)
        {
            throw new EmailSenderException("Something went wrong with email password reset confirmation. Please resend email");
        }
        response.Success = true;
        return response;

    }
}

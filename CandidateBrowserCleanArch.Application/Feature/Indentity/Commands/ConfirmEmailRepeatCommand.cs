using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CandidateBrowserCleanArch.Application;

public class ConfirmEmailRepeatCommand:IRequest<ServiceReponse<bool>>
{
    public ConfirmEmailRepeatRequest ConfirmEmailRepeatRequest { get; set; }
    public string RequestUrl { get; set; }
}

public class ConfirmEmailRepeatCommandHandler : IRequestHandler<ConfirmEmailRepeatCommand, ServiceReponse<bool>>
{
    private readonly IAuthService _authService;
    private readonly IEmailSenderService _emailSenderService;

    public ConfirmEmailRepeatCommandHandler(IAuthService authService, IEmailSenderService emailSenderService)
    {
        _authService = authService;
        _emailSenderService = emailSenderService;
    }
    public async Task<ServiceReponse<bool>> Handle(ConfirmEmailRepeatCommand request, CancellationToken cancellationToken)
    {
        var response=new ServiceReponse<bool>();
        var validator = new ConfirmEmailRepeatRequestValidator();
        var validationResult = await validator.ValidateAsync(request.ConfirmEmailRepeatRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }
        var result = await _authService.ConfirmEmailRepeat(request.ConfirmEmailRepeatRequest);
        if (!result.Success)
        {
            throw new BadRequestException(result.Message);
        }

        var requestUrl = $"{request.RequestUrl}userId={HttpUtility.UrlEncode(result.EncryptedUserId)}&token={HttpUtility.UrlEncode(result.ValidToken)}&redirectUrl={request.ConfirmEmailRepeatRequest.ReturnUrl}";
        var message = "<p>Welcome to Candidates Browser </p> <p>Please confirm you email account by clicking this link <br> <a href=\"" + requestUrl + "\" >Click here </a> </p>";
        var emailRequest = await _emailSenderService.SendEmailAsync(request.ConfirmEmailRepeatRequest.Email, "Candidates Browser registration", message);
        if (!emailRequest)
        {
            throw new EmailSenderException("Something went wrong with email confirmation. Please resend email");
        }
        response.Success=true;
        return response;


    }
}
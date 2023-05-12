using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class ConfirmEmailCommand:IRequest<ServiceReponse<bool>> 
{
    public ConfirmEmailRequest ConfirmEmailRequest { get; set; }
}

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ServiceReponse<bool>>
{
    private readonly IAuthService _authService;

    public ConfirmEmailCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task<ServiceReponse<bool>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var response= new ServiceReponse<bool>();
        var validator = new ConfirmEmailRequestValidator();
        var validationResult = await validator.ValidateAsync(request.ConfirmEmailRequest, cancellationToken);
        if (!validationResult.IsValid)
        {
            return response;
        }
        response = await _authService.ConfirmEmail(request.ConfirmEmailRequest);
        return response;
    }
}

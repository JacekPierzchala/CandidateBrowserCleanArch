using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class RegisterCommand:IRequest<RegistrationResponse> 
{
    public RegistrationRequest RegistrationRequest { get; set; }
}

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegistrationResponse>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task<RegistrationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response =await _authService.Register(request.RegistrationRequest);
        if (!response.Success)
        {
            throw new RegistrationException(response.Errors);
        }
        return response;
    }
}


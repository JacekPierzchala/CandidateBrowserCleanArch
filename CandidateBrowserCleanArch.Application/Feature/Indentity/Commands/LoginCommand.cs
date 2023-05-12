using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class LoginCommand:IRequest<AuthResponse>
{
    public AuthRequest AuthRequest { get; set; }
}
internal class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validator = new AuthRequestValidator();
        var validationResult=await validator.ValidateAsync(request.AuthRequest, cancellationToken);

        if(!validationResult.IsValid) 
        { 
            throw new ValidationException(validationResult);
        }

        var response=await _authService.Login(request.AuthRequest);
        if(!response.Success)
        {
            throw new AuthorizationException(response.Message);
        }
        return response;
    }
}

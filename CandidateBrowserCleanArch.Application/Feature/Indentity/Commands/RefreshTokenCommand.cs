using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;
public class RefreshTokenCommand:IRequest<AuthResponse>
{
    public RefreshTokenRequest RefreshTokenRequest { get; set; }
}
public class RefreshTokenCommandHandler:IRequestHandler<RefreshTokenCommand,AuthResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenCommandHandler(IAuthService authService)
	{
        _authService = authService;
    }

    public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var response= new AuthResponse();
        var validator= new RefreshTokenRequestValidator();
        var validationResult=await validator.ValidateAsync(request.RefreshTokenRequest, cancellationToken);
        if(!validationResult.IsValid) 
        {
            throw new ValidationException(validationResult);
        }
        response=await _authService.RefreshToken(request.RefreshTokenRequest);
        if(!response.Success) 
        {
            throw new AuthorizationException(response.Message);
        }
        return response;
    }
}

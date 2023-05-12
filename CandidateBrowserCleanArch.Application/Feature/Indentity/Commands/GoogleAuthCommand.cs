using MediatR;

namespace CandidateBrowserCleanArch.Application;


public class GoogleAuthCommand:IRequest<AuthResponse>
{
    public string AuthCode { get; set; }
    public string RedirectUrl { get; set; }

}
public class GoogleAuthCommandHandler:IRequestHandler<GoogleAuthCommand, AuthResponse>
{
    private readonly IAuthService _authService;

    public GoogleAuthCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task<AuthResponse>Handle(GoogleAuthCommand request ,CancellationToken cancellationToken)
    {
        var response = await _authService.AuthWithGoogle(request.AuthCode,request.RedirectUrl);       
        return response;
    }
}

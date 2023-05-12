using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public sealed class ResetPasswordCommand : IRequest<ServiceReponse<bool>>
{
    public ResetPasswordRequest ResetPasswordRequest { get; set; }
}

public sealed class ResetPasswordCommandHandler :
                IRequestHandler<ResetPasswordCommand, ServiceReponse<bool>>
{
    private readonly IAuthService _authService;

    public ResetPasswordCommandHandler(IAuthService authService)
        => _authService = authService;

    public async Task<ServiceReponse<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<bool>();
        var validator = new ResetPasswordRequestValidator();
        var validationResult = await validator.ValidateAsync(request.ResetPasswordRequest, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }
        response = await _authService.ResetPassword(request.ResetPasswordRequest);


        return response;
    }
}
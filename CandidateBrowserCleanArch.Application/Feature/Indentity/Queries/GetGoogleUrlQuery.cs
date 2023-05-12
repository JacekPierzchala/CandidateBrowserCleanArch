using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class GetGoogleUrlQuery:IRequest<ServiceReponse<string>>
{
    public string RedirectUrl { get; set; }
}
public class GetGoogleUrlQueryHandler : IRequestHandler<GetGoogleUrlQuery, ServiceReponse<string>>
{
    private readonly IAuthService _authService;

    public GetGoogleUrlQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task<ServiceReponse<string>> Handle(GetGoogleUrlQuery request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<string>();
        var result= await _authService.GetGoogleAuthUrl(request.RedirectUrl);
        response.Data = result.Data;
        response.Success = result.Success;
        return response;
    }
}

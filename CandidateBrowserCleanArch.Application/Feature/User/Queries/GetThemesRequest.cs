using AutoMapper;
using MediatR;

namespace CandidateBrowserCleanArch.Application;

public class GetThemesRequest:IRequest<ServiceReponse<IEnumerable<ConfigThemeDto>>>
{

}
public class GetThemesRequestHandler : 
    IRequestHandler<GetThemesRequest, ServiceReponse<IEnumerable<ConfigThemeDto>>>
{
    private readonly IMapper _mapper;
    private readonly IConfigThemeRepository _configThemeRepository;

    public GetThemesRequestHandler(IMapper mapper, IConfigThemeRepository configThemeRepository)
	{
        _mapper = mapper;
        _configThemeRepository = configThemeRepository;
    }

    public async Task<ServiceReponse<IEnumerable<ConfigThemeDto>>> 
        Handle(GetThemesRequest request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<IEnumerable<ConfigThemeDto>>();

        var dbData = await _configThemeRepository.GetActiveThemesAsync(cancellationToken);
        response.Data = _mapper.Map<IEnumerable<ConfigThemeDto>>(dbData);
        response.Success = true;

        return response;
    }
}

using AutoMapper;
using MediatR;

namespace CandidateBrowserCleanArch.Application;

public class GetUserSettingsRequest:IRequest<ServiceReponse<UserSettingsDto?>>
{
    public string UserId { get; set; }
}

public class GetUserSettingsRequestHandler :
    IRequestHandler<GetUserSettingsRequest, ServiceReponse<UserSettingsDto?>>
{
    private readonly IMapper _mapper;
    private readonly IUserSettingsRepository _userSettingsRepository;

    public GetUserSettingsRequestHandler(IMapper mapper, 
        IUserSettingsRepository userSettingsRepository)
    {
        _mapper = mapper;
        _userSettingsRepository = userSettingsRepository;
    }
    public async Task<ServiceReponse<UserSettingsDto?>> Handle(GetUserSettingsRequest request, CancellationToken cancellationToken)
    {
        var response= new ServiceReponse<UserSettingsDto?>();

        var dbData = await _userSettingsRepository.GetUserSettingsAsync(request.UserId);
        response.Data = _mapper.Map<UserSettingsDto>(dbData);
        response.Success = true;

        return response;
    }
}
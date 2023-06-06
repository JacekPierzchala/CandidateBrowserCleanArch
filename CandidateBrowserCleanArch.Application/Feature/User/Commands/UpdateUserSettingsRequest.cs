using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using MediatR;

namespace CandidateBrowserCleanArch.Application;

public class UpdateUserSettingsRequest:IRequest<ServiceReponse<bool>>
{
    public string UserId { get; set; }
    public UserSettingsDto UserSettingsDto { get; set; }
}
public class UpdateUserSettingsRequestHandler :
    IRequestHandler<UpdateUserSettingsRequest, ServiceReponse<bool>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserSettingsRequestHandler(IUserRepository userRepository, 
        IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceReponse<bool>> 
        Handle(UpdateUserSettingsRequest request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<bool>();
        if (request.UserId != request.UserSettingsDto.UserId)
        {
            throw new BadRequestException("Inconsistent Id");
        }
        var validator = new UserSettingsDtoValidator(_unitOfWork.ConfigThemeRepository);
        var validationResult = await validator.ValidateAsync(request.UserSettingsDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }
        var user = await _userRepository.GetUser(request.UserId);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.UserId);
        }

        var userSettings= await _unitOfWork.UserSettingsRepository.GetUserSettingsAsync(request.UserId);
        if (userSettings == null) 
        {
            userSettings=_mapper.Map<UserSettings>(request.UserSettingsDto);
            await _unitOfWork.UserSettingsRepository.AddAsync(userSettings);
        }
        else
        {
             _mapper.Map(request.UserSettingsDto,userSettings);
            await _unitOfWork.UserSettingsRepository.UpdateAsync(userSettings);
        }

        var result = await _unitOfWork.SaveAsync();
        response.Success = result;
        if(response.Success)
        {
            response.Message = "User settings update success";
            response.Data = response.Success;
        }
        else
        {
            response.Message = "User settings update falied";
        }

        return response;
    }
}

using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class UpdateUserRequest:IRequest<ServiceReponse<bool>>
{
    public string UserId { get; set; }
    public UpdateUserDto UpdateUser { get; set; }
}
public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, ServiceReponse<bool>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;

    public UpdateUserRequestHandler(IUserRepository userRepository,
        IMapper mapper, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _roleRepository = roleRepository;
    }
    public async Task<ServiceReponse<bool>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<bool>();
        if (request.UserId != request.UpdateUser.Id)
        {
            throw new BadRequestException("Inconsistent Id");
        }
        var validator = new UpdateUserDtoValidator(_roleRepository);
        var validationResult = await validator.ValidateAsync(request.UpdateUser, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }

        var user = await _userRepository.GetUser(request.UserId);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.UserId);
        }

        _mapper.Map(request.UpdateUser, user);
        var result= await _userRepository.UpdateUser(user);
        response.Success = result.result;
        response.Message = result.message;
        response.Data = result.result;
        return response;

    }
}
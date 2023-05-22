using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class UpdateCandidateCommand:IRequest<ServiceReponse<CandidateDetailsDto>>
{
    public CandidateUpdateDto CandidateUpdate { get; set; }
    public int Id { get; set; }
}
public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, ServiceReponse<CandidateDetailsDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPictureStorageService _pictureStorageService;

    public UpdateCandidateCommandHandler(IMapper mapper,
        IUnitOfWork unitOfWork,
        IPictureStorageService pictureStorageService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _pictureStorageService = pictureStorageService;
    }
    public async Task<ServiceReponse<CandidateDetailsDto>> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<CandidateDetailsDto>();
        if(request.Id!= request.CandidateUpdate.Id) 
        {
            throw new BadRequestException("Inconsistent Id");
        }
        var validator = new CandidateUpdateDtoValidator();
        var validationResult = await validator.ValidateAsync(request.CandidateUpdate, cancellationToken);
        if(!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }
        var candidate = await _unitOfWork.CandidateRepository.GetAsync(request.Id);
        if (candidate==null)
        {
            throw new NotFoundException(nameof(request.CandidateUpdate.Email), request.Id);
        }

        _mapper.Map(request.CandidateUpdate, candidate);

        candidate.ProfilePicture=await _pictureStorageService.UploadPicture
                (request.CandidateUpdate.ProfilePictureData, request.CandidateUpdate.ProfilePicture, request.CandidateUpdate.ProfilePictureOld);

        candidate.ProfilePicture = !string.IsNullOrEmpty(candidate.ProfilePicture) ? candidate.ProfilePicture : "avatar.png";
        await _unitOfWork.CandidateRepository.UpdateAsync(candidate);
        
        response.Success = await _unitOfWork.SaveAsync();

        if (response.Success)
        {
            response.Data = _mapper.Map<CandidateDetailsDto>(await _unitOfWork.CandidateRepository.GetCandidateWithDetailsAsync(candidate.Id));
            response.Data.ProfilePath = await _pictureStorageService.GetPicture(response.Data.ProfilePicture);
        }
        else
        {
            throw new Exception("Error occurred during this operation");
        }

        return response;
    }
}

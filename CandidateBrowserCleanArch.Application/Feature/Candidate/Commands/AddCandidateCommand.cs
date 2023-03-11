using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class AddCandidateCommand:IRequest<ServiceReponse<CandidateDetailsDto>>
{
    public CandidateCreateDto CreateCandidateDto { get; set; }
}
public class AddCandidateCommandHandler : IRequestHandler<AddCandidateCommand, ServiceReponse<CandidateDetailsDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AddCandidateCommandHandler(
        IMapper mapper, 
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<ServiceReponse<CandidateDetailsDto>> Handle(AddCandidateCommand request, CancellationToken cancellationToken)
    {
        var response= new ServiceReponse<CandidateDetailsDto>();
        var validator = new CreateCandidateDtoValidator();
        var validationResult = await validator.ValidateAsync(request.CreateCandidateDto, cancellationToken);
        if(!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }
        var candidate = _mapper.Map<Candidate>(request.CreateCandidateDto);
        await _unitOfWork.CandidateRepository.AddAsync(candidate);

        response.Success=await _unitOfWork.SaveAsync();
        if(response.Success)
        {         
            response.Data = _mapper.Map <CandidateDetailsDto>( await _unitOfWork.CandidateRepository.GetCandidateWithDetailsAsync(candidate.Id));
        }    
        else
        {
            throw new Exception("Error occurred during this operation");
        }

        return response;
    }
}
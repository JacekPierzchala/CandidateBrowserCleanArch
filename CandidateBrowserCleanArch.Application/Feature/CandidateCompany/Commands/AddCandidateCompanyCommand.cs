using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class AddCandidateCompanyCommand:IRequest<ServiceReponse<CandidateCompanyDto>> 
{
    public CandidateCompanyAddDto CandidateCompanyAddDto { get; set; }
}

public class AddCandidateCompanyCommandHandler : 
    IRequestHandler<AddCandidateCompanyCommand, ServiceReponse<CandidateCompanyDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AddCandidateCompanyCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<ServiceReponse<CandidateCompanyDto>> Handle(AddCandidateCompanyCommand request, CancellationToken cancellationToken)
    {
        var response=new ServiceReponse<CandidateCompanyDto>();
        var validator = new CandidateCompanyAddDtoValidator(_unitOfWork.CandidateRepository,_unitOfWork.CompanyRepository);
        var validationResult = await validator.ValidateAsync(request.CandidateCompanyAddDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }

        var candidateCompany = _mapper.Map<CandidateCompany>(request.CandidateCompanyAddDto);
        await _unitOfWork.CandidateCompanyRepository.AddAsync(candidateCompany);
        response.Success = await _unitOfWork.SaveAsync();
        if (response.Success)
        {
            response.Data = _mapper.Map<CandidateCompanyDto>
                (await _unitOfWork.CandidateCompanyRepository.GetCandidateCompanyDetailsAsync(candidateCompany.Id, cancellationToken));
        }
        else
        {
            throw new Exception("Error occurred during this operation");
        }

        return response;
    }
}
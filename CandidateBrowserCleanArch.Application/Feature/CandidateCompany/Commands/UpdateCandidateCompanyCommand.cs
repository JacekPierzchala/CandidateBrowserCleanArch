using AutoMapper;
using CandidateBrowserCleanArch.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class UpdateCandidateCompanyCommand:IRequest<ServiceReponse<CandidateCompanyDto>>
{
    public int Id { get; set; }
    public CandidateCompanyUpdateDto CandidateCompanyUpdateDto { get; set; }
}
public class UpdateCandidateCompanyCommandHandler :
    IRequestHandler<UpdateCandidateCompanyCommand, ServiceReponse<CandidateCompanyDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCandidateCompanyCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ServiceReponse<CandidateCompanyDto>> Handle(UpdateCandidateCompanyCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<CandidateCompanyDto>();
   
        if (request.Id != request.CandidateCompanyUpdateDto.Id)
        {
            throw new BadRequestException("Inconsistent Id");
        }
        var validator = new CandidateCompanyUpdateDtoValidator(_unitOfWork.CandidateRepository, _unitOfWork.CompanyRepository);
        var validationResult = await validator.ValidateAsync(request.CandidateCompanyUpdateDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }
        var candidateCompany = await _unitOfWork.CandidateCompanyRepository.GetAsync(request.Id);
        if (candidateCompany == null)
        {
            throw new NotFoundException(nameof(CandidateCompany), request.Id);
        }

        _mapper.Map(request.CandidateCompanyUpdateDto, candidateCompany);
        await _unitOfWork.CandidateCompanyRepository.UpdateAsync(candidateCompany);

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
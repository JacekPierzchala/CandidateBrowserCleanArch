using CandidateBrowserCleanArch.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Application;

public class DeleteCandidateCompanyCommand:IRequest<ServiceReponse<bool>>
{
    public int Id { get; set; }
}
public class DeleteCandidateCompanyCommandHandler
    : IRequestHandler<DeleteCandidateCompanyCommand, ServiceReponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCandidateCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ServiceReponse<bool>> Handle(DeleteCandidateCompanyCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<bool>();
        var candidateCompany = await _unitOfWork.CandidateCompanyRepository.GetAsync(request.Id);
        if (candidateCompany == null)
        {
            throw new NotFoundException(nameof(CandidateCompany), request.Id);
        }
        await _unitOfWork.CandidateCompanyRepository.DeleteAsync(candidateCompany);

        response.Success = await _unitOfWork.SaveAsync();
        if (!response.Success)
        {
            throw new Exception("Error occurred during this operation");
        }
        response.Data = true;
        return response;
    }
}
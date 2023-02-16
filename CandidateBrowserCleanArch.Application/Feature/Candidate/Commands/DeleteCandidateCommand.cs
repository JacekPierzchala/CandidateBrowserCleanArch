using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateBrowserCleanArch.Applicationl
{
    public class DeleteCandidateCommand:IRequest<BaseResponse>
    {
        public int CandidateId { get; set; }
    }
    internal sealed class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, BaseResponse>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCandidateCommandHandler(ICandidateRepository candidateRepository,
            IUnitOfWork unitOfWork)
        {
            _candidateRepository = candidateRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var response = new ServiceReponse<bool>();
            var candidate = await _candidateRepository.GetAsync(request.CandidateId);
            if (candidate == null)
            {
                throw new NotFoundException(nameof(Candidate), request.CandidateId);
            }
            await _candidateRepository.DeleteCandidateAsync(request.CandidateId);
            
            response.Success=await _unitOfWork.SaveAsync();
            if(!response.Success) 
            {
                throw new Exception("Error occurred during this operation");
            }
            
            return response;
        }
    }
}

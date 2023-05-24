using CandidateBrowserCleanArch.Application;
using CandidateBrowserCleanArch.Domain;
using MediatR;

namespace CandidateBrowserCleanArch.Applicationl
{
    public class DeleteCandidateCommand:IRequest<BaseResponse>
    {
        public int CandidateId { get; set; }
    }
    public sealed class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCandidateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var response = new ServiceReponse<bool>();
            var candidate = await _unitOfWork.CandidateRepository.GetAsync(request.CandidateId);
            if (candidate == null)
            {
                throw new NotFoundException(nameof(Candidate), request.CandidateId);
            }
            await _unitOfWork.CandidateRepository.DeleteCandidateAsync(request.CandidateId);
            
            response.Success=await _unitOfWork.SaveAsync();
            if(!response.Success) 
            {
                throw new Exception("Error occurred during this operation");
            }
            response.Data = true;
            return response;
        }
    }
}

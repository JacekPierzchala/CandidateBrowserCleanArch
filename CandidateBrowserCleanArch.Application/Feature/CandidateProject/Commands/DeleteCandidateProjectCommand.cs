using CandidateBrowserCleanArch.Domain;
using MediatR;

namespace CandidateBrowserCleanArch.Application;

public class DeleteCandidateProjectCommand: IRequest<ServiceReponse<bool>>
{
    public int Id { get; set; }
}
public class DeleteCandidateProjectCommandHandler :
    IRequestHandler<DeleteCandidateProjectCommand, ServiceReponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCandidateProjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ServiceReponse<bool>> Handle(DeleteCandidateProjectCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceReponse<bool>();
        var candidateProject = await _unitOfWork.CandidateProjectRepository.GetAsync(request.Id);
        if (candidateProject == null)
        {
            throw new NotFoundException(nameof(CandidateProject), request.Id);
        }
        await _unitOfWork.CandidateProjectRepository.DeleteAsync(candidateProject);

        response.Success = await _unitOfWork.SaveAsync();
        if (!response.Success)
        {
            throw new Exception("Error occurred during this operation");
        }
        response.Data = true;
        return response;
    }
}
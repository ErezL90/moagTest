using MediatR;
using Assignments.API.BL.Services;
using Assignments.API.BL.CQRS.Command;

namespace Assignments.API.BL.CQRS.Handlers
{
    public class DeleteAssignmentHandler : IRequestHandler<DeleteAssignmentCommand, int>
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public DeleteAssignmentHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public async Task<int> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _assignmentRepository.GetAssignmentByIdAsync(request.AssignmentId);
            if (assignment == null) return default;
            return await _assignmentRepository.DeleteAssignmentAsync(request.AssignmentId);
        }
    }
}

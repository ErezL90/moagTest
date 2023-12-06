using Assignments.API.BL.CQRS.Command;
using Assignments.API.BL.Services;
using MediatR;

namespace Assignments.API.BL.CQRS.Handlers
{
    public class UpdateAssignmentEndHandler : IRequestHandler<UpdateEndAssignmentsCommand, int>
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public UpdateAssignmentEndHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public async Task<int> Handle(UpdateEndAssignmentsCommand request, CancellationToken cancellationToken)
        {
            return await _assignmentRepository.UpdateEndAssignments();
        }
    }
}

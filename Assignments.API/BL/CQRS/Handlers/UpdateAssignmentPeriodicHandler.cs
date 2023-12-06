using Assignments.API.BL.CQRS.Command;
using Assignments.API.BL.Enums;
using Assignments.API.BL.Services;
using MediatR;

namespace Assignments.API.BL.CQRS.Handlers
{
    public class UpdateAssignmentPeriodicHandler : IRequestHandler<UpdateAssignmentPeriodicCommand, int>
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public UpdateAssignmentPeriodicHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public async Task<int> Handle(UpdateAssignmentPeriodicCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _assignmentRepository.GetAssignmentByIdAsync(request.AssignmentId);
            if (assignment == null || String.IsNullOrEmpty(assignment.AssignmentName)) return default;

            return await _assignmentRepository.UpdateAssignmentAsync(request.AssignmentId, UpdateType.Periodic, request.IsPeriodic);
        }
    }
}

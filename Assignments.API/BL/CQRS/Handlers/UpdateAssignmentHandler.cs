using MediatR;
using Assignments.API.BL.CQRS.Command;
using Assignments.API.BL.Services;

namespace Assignments.API.BL.CQRS.Handlers
{
    public class UpdateAssignmentHandler : IRequestHandler<UpdateAssignmentCommand, int>
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public UpdateAssignmentHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public async Task<int> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _assignmentRepository.GetAssignmentByIdAsync(request.AssignmentId);
            if (assignment == null || String.IsNullOrEmpty(assignment.AssignmentName)) return default;
            assignment.AssignmentName = request.AssignmentName;
            assignment.Description = request.Description;
            assignment.StartDate = request.StartDate;
            assignment.EndDate = request.EndDate;
            assignment.IsPeriodic = request.IsPeriodic;
            assignment.IsCompleted = request.IsCompleted;
            assignment.IsArchive = request.IsArchive;
            assignment.AssignmentTypeId = request.AssignmentTypeId;
            
            return await _assignmentRepository.UpdateAssignmentAsync(assignment);
        }
    }
}

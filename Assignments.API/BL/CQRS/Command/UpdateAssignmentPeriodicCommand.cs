using MediatR;

namespace Assignments.API.BL.CQRS.Command
{
    public class UpdateAssignmentPeriodicCommand : IRequest<int>
    {
        public UpdateAssignmentPeriodicCommand(int assignmentId, bool isPeriodic)
        {
            AssignmentId = assignmentId;
            IsPeriodic = isPeriodic;
        }
        public int AssignmentId { get; set; }
        public bool IsPeriodic { get; set; }
    }
}

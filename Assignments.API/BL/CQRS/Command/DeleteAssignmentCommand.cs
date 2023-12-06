using MediatR;

namespace Assignments.API.BL.CQRS.Command
{
    public class DeleteAssignmentCommand : IRequest<int>
    {
        public int AssignmentId { get; set; }
    }
}

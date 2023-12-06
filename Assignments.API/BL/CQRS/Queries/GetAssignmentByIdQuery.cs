using MediatR;
using Assignments.API.Data.Models;
using Assignments.API.BL.DTO;

namespace Assignments.API.BL.CQRS.Queries
{
    public class GetAssignmentByIdQuery : IRequest<Assignment>
    {
        public int AssignmentId { get; set; }
    }
}

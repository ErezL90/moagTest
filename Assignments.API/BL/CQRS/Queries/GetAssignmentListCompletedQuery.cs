using MediatR;
using Assignments.API.Data.Models;
using Assignments.API.BL.DTO;

namespace Assignments.API.BL.CQRS.Queries
{
    public class GetAssignmentListCompletedQuery : IRequest<List<AssignmentRes>>
    {
    }
}

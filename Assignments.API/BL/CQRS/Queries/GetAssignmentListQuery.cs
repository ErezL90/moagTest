using Assignments.API.BL.DTO;
using MediatR;

namespace Assignments.API.BL.CQRS.Queries
{
    public class GetAssignmentListQuery: IRequest<List<AssignmentRes>>
    {
    }
}
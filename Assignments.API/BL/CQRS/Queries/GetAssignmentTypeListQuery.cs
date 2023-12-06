using Assignments.API.Data.Models;
using MediatR;

namespace Assignments.API.BL.CQRS.Queries
{
    public class GetAssignmentTypeListQuery : IRequest<List<AssignmentType>>
    {
    }
}


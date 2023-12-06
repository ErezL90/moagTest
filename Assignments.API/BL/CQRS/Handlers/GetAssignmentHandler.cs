using MediatR;
using Assignments.API.Data.Models;
using Assignments.API.BL.Services;
using Assignments.API.BL.CQRS.Queries;
using Assignments.API.BL.DTO;

namespace Assignments.API.BL.CQRS.Handlers
{
    public class GetAssignmentHandler : IRequestHandler<GetAssignmentByIdQuery, Assignment>
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public GetAssignmentHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public async Task<Assignment> Handle(GetAssignmentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _assignmentRepository.GetAssignmentByIdAsync(request.AssignmentId);
        }
    }
}

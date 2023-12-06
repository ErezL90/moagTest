using MediatR;
using Assignments.API.Data.Models;
using Assignments.API.BL.Services;
using Assignments.API.BL.CQRS.Queries;
using Assignments.API.BL.DTO;

namespace Assignments.API.BL.CQRS.Handlers
{
    public class GetAssignmentListByDateHandler : IRequestHandler<GetAssignmentListByDateQuery, List<AssignmentRes>>
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public GetAssignmentListByDateHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public async Task<List<AssignmentRes>> Handle(GetAssignmentListByDateQuery request, CancellationToken cancellationToken)
        {
            return await _assignmentRepository.GetAssignmentListByDateAsync();
         
        }
    }
}

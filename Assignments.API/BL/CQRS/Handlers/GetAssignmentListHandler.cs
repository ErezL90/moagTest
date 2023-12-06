using Assignments.API.BL.CQRS.Queries;
using Assignments.API.BL.DTO;
using Assignments.API.BL.Services;
using MediatR;

namespace Assignments.API.BL.CQRS.Handlers
{
    public class GetAssignmentListHandler : IRequestHandler<GetAssignmentListQuery, List<AssignmentRes>>
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public GetAssignmentListHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public async Task<List<AssignmentRes>> Handle(GetAssignmentListQuery request, CancellationToken cancellationToken)
        {
            return await _assignmentRepository.GetAssignmentListAsync();

        }
    }
}



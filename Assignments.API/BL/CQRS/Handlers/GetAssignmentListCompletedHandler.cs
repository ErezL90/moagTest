using Assignments.API.BL.CQRS.Queries;
using Assignments.API.BL.DTO;
using Assignments.API.BL.Services;
using Assignments.API.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignments.API.BL.CQRS.Handlers
{
    public class GetAssignmentListCompletedHandler : IRequestHandler<GetAssignmentListCompletedQuery, List<AssignmentRes>>
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public GetAssignmentListCompletedHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        
        public async Task<List<AssignmentRes>> Handle(GetAssignmentListCompletedQuery request, CancellationToken cancellationToken)
        {
            return await _assignmentRepository.GetAssignmentListCompletedAsync();
        }
    }
}

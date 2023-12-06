using Assignments.API.BL.CQRS.Queries;
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
    internal class GetAssignmentTypeListHandler : IRequestHandler<GetAssignmentTypeListQuery, List<AssignmentType>>
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public GetAssignmentTypeListHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task<List<AssignmentType>> Handle(GetAssignmentTypeListQuery request, CancellationToken cancellationToken)
        {
            return await _assignmentRepository.GetAssignmentTypeListAsync();
        }
    }

}

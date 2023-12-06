using MediatR;
using Assignments.API.BL.Services;
using Assignments.API.BL.CQRS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignments.API.BL.Enums;

namespace Assignments.API.BL.CQRS.Handlers
{
    public class UpdateAssignmentCompleteHandler : IRequestHandler<UpdateAssignmentCompleteCommand, int>
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public UpdateAssignmentCompleteHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public async Task<int> Handle(UpdateAssignmentCompleteCommand request, CancellationToken cancellationToken)
        {
            return await _assignmentRepository.UpdateAssignmentsCompletedAsync(request.ArrayCompleted);
        }
    }

}

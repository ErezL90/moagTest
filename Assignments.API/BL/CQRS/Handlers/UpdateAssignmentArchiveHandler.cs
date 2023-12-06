using Assignments.API.BL.Enums;
using Assignments.API.BL.CQRS.Command;
using Assignments.API.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Assignments.API.BL.CQRS.Handlers
{
    public class UpdateAssignmentArchiveHandler : IRequestHandler<UpdateAssignmentArchiveCommand, int>
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public UpdateAssignmentArchiveHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public async Task<int> Handle(UpdateAssignmentArchiveCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _assignmentRepository.GetAssignmentByIdAsync(request.AssignmentId);
            if (assignment == null || String.IsNullOrEmpty(assignment.AssignmentName)) return default;

            return await _assignmentRepository.UpdateAssignmentAsync(request.AssignmentId, UpdateType.Archive, request.IsArchived);
        }

    }
}

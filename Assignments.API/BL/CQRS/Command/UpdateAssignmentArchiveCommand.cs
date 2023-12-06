using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignments.API.BL.CQRS.Command
{
    public class UpdateAssignmentArchiveCommand : IRequest<int>
    {
        public UpdateAssignmentArchiveCommand(int assignmentId, bool isArchived)
        {
            AssignmentId = assignmentId;
            IsArchived = isArchived;
        }
        public int AssignmentId { get; set; }
        public bool IsArchived { get; set; }
    }
}

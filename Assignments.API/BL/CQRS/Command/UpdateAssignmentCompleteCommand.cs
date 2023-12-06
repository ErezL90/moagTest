using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignments.API.BL.CQRS.Command
{
    public class UpdateAssignmentCompleteCommand : IRequest<int>
    {
        public UpdateAssignmentCompleteCommand(string arrayCompleted)
        {
            ArrayCompleted = arrayCompleted;
        }
        public string ArrayCompleted { get; set; }
    }
}

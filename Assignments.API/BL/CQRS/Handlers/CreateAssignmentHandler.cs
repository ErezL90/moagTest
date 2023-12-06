using MediatR;
using Assignments.API.Data.Models;
using Assignments.API.BL.Services;
using Assignments.API.BL.CQRS.Command;
using Assignments.API.BL.DTO;

namespace Assignments.API.BL.CQRS.Handlers
{
    public class CreateAssignmentHandler : IRequestHandler<CreateAssignmentCommand, AssignmentRes>
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public CreateAssignmentHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public Task<AssignmentRes> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
        {
            Assignment assignment = new()
            {
                AssignmentName = request.AssignmentName,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsPeriodic = request.IsPeriodic,
                AssignmentTypeId = request.AssignmentTypeId
            };

            return _assignmentRepository.AddAssignmentAsync(assignment);
        }
    }
}

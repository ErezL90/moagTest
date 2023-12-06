using MediatR;
using Assignments.API.Data.Models;
using Assignments.API.BL.DTO;

namespace Assignments.API.BL.CQRS.Command
{
    public class CreateAssignmentCommand : IRequest<AssignmentRes>
    {
        public CreateAssignmentCommand(CreateAssignmentItemDto dto)
        {
            AssignmentName = dto.AssignmentName;
            Description = dto.Description;
            StartDate = dto.StartDate;               
            EndDate = dto.EndDate;
            IsPeriodic = dto.IsPeriodic;
            AssignmentTypeId = dto.AssignmentTypeId;
        }
        public string AssignmentName { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPeriodic { get; set; }
        public int AssignmentTypeId { get; set; }
    }
}

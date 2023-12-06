using Assignments.API.BL.DTO;
using MediatR;

namespace Assignments.API.BL.CQRS.Command
{
    public class UpdateAssignmentCommand : IRequest<int>
    {
        public UpdateAssignmentCommand(UpdateAssignmentItemDto dto)
        {
            AssignmentId = dto.AssignmentId;
            AssignmentName = dto.AssignmentName;
            Description = dto.Description;
            StartDate = dto.StartDate;
            EndDate = dto.EndDate;
            IsPeriodic = dto.IsPeriodic;
            IsCompleted = dto.IsCompleted;
            IsArchive = dto.IsArchive;
            AssignmentTypeId = dto.AssignmentTypeId;
        }
        public int AssignmentId { get; set; }
        public string AssignmentName { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPeriodic { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsArchive { get; set; }
        public int AssignmentTypeId { get; set; } 
    }
}

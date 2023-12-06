namespace Assignments.API.BL.DTO 
{
    public class UpdateAssignmentItemDto
    {
        public int AssignmentId { get; set; }
        public string AssignmentName { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPeriodic { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        public bool IsArchive { get; set; } = false;
        public int AssignmentTypeId { get; set; } 
    }
}

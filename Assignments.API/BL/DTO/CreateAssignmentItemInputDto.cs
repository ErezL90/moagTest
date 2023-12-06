namespace Assignments.API.BL.DTO
{
    public class CreateAssignmentItemInputDto
    {
        public string AssignmentName { get; set; } = default!;
        public string? Description { get; set; }
        public string StartDate { get; set; } = default!;
        public string EndDate { get; set; } = default!;
        public bool IsPeriodic { get; set; } = false;
        public int AssignmentTypeId { get; set; } //FK
    }
}

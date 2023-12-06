
namespace Assignments.API.BL.DTO
{
    public class AssignmentRes
    {
        public int AssignmentId { get; set; }
        public string? AssignmentName { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPeriodic { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsArchive { get; set; }
        public int AssignmentTypeId { get; set; }
    }
}
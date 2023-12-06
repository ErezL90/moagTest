namespace Assignments.API.BL.DTO
{
    public class ReqAssignmentsDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Sort { get; set; }
        public Boolean Order { get; set; }
    }
}

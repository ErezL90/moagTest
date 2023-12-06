namespace Assignments.API.BL.DTO
{
    public class AssignmentResPagination
    {
        public List<AssignmentRes> AssignmentRes { get; set; } = default!;
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

    }
}

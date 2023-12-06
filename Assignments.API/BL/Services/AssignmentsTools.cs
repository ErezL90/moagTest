using Assignments.API.BL.DTO;

namespace Assignments.API.BL.Services
{
    public static class AssignmentsTools
    {
        public static AssignmentResPagination GetAssignmentsResPagination(List<AssignmentRes> assignmentRes, int page, int pageSize)
        {
            var assignmentResPagination = new AssignmentResPagination();
            assignmentResPagination.AssignmentRes = assignmentRes;
            assignmentResPagination.TotalRecords = assignmentRes.Count();
            assignmentResPagination.TotalPages = (int)Math.Ceiling(assignmentRes.Count() / (double)pageSize);
            assignmentResPagination.AssignmentRes = assignmentResPagination.AssignmentRes.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return assignmentResPagination;
        }

        public static List<AssignmentRes> GetSortResults(List<AssignmentRes> list, string sortby, bool asc)
        {
            sortby = char.ToUpper(sortby[0]) + sortby.Substring(1);

            var property = typeof(AssignmentRes).GetProperty(sortby);

            if (property == null)
                return list;

            var orderedList = asc ? list.OrderBy(x => property.GetValue(x, null)).ToList() : list.OrderByDescending(x => property.GetValue(x, null)).ToList();

            return orderedList;
        }

        public static List<AssignmentRes> GetArchiveResults(List<AssignmentRes> list, bool archive)
        {
            return  archive ? list : list.Where(x => !x.IsArchive).ToList()  ;
        }

    }
}

using Assignments.API.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Assignments.API.BL.DTO
{
    public static class AssignmentMapper
    {

        public static AssignmentRes MapToAssignmentFiltered(Assignment assignment)
        {
            return new AssignmentRes
            {
                AssignmentId = assignment.AssignmentId,
                AssignmentName = assignment.AssignmentName,
                Description = assignment.Description ?? "",
                StartDate = assignment.StartDate,
                EndDate = assignment.EndDate,
                IsPeriodic = assignment.IsPeriodic,
                IsCompleted = assignment.IsCompleted,
                IsArchive = assignment.IsArchive,
                AssignmentTypeId = assignment.AssignmentTypeId
            };
        }

        public static List<AssignmentRes> MapToAssignmentFiltered(IEnumerable<Assignment> assignments)
        {
            return assignments.Select(MapToAssignmentFiltered).ToList();
        }

        public static CreateAssignmentItemDto MapToCreateAssignment(CreateAssignmentItemInputDto dto)
        {
            string format = "yyyy-dd-MMTHH:mm:ss";

            try
            {
                DateTime startDate = DateTime.ParseExact(dto.StartDate, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                DateTime endDate = DateTime.ParseExact(dto.EndDate, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

                return new CreateAssignmentItemDto
                {
                    AssignmentName = dto.AssignmentName,
                    Description = dto.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    IsPeriodic = dto.IsPeriodic,
                    AssignmentTypeId = dto.AssignmentTypeId
                };

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

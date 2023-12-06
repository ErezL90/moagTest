using Assignments.API.BL.DTO;
using Assignments.API.BL.Enums;
using Assignments.API.Data.Models;

namespace Assignments.API.BL.Services
{
    public interface IAssignmentRepository
    {  
        public Task<List<AssignmentType>> GetAssignmentTypeListAsync();
        public Task<List<AssignmentRes>> GetAssignmentListAsync(); 
        public Task<List<AssignmentRes>> GetAssignmentListByDateAsync(); 
        public Task<List<AssignmentRes>> GetAssignmentListCompletedAsync(); 
        public Task<Assignment> GetAssignmentByIdAsync(int id); 
        public Task<AssignmentRes> AddAssignmentAsync(Assignment assignment);
        public Task<int> UpdateAssignmentsCompletedAsync(string arrAssignments);
        public Task<int> UpdateAssignmentAsync(Assignment assignment);
        public Task<int> UpdateAssignmentAsync<T>(int assignmentId, UpdateType updateType, T arg); 
        public Task<int> DeleteAssignmentAsync(int id);
        public Task<int> UpdateEndAssignments();
    }
}

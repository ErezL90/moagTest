using Assignments.API.BL.DTO;
using Assignments.API.BL.Enums;
using Assignments.API.Context;
using Assignments.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignments.API.BL.Services
{
    public class AssignmentRepository : IAssignmentRepository
    {

        private readonly ApplicationDbContext _dbContext;
        private ILogger<AssignmentRepository> _logger;
        public AssignmentRepository(ApplicationDbContext context, ILogger<AssignmentRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        /// <summary>
        /// CRUD operations
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns></returns>
        public async Task<AssignmentRes> AddAssignmentAsync(Assignment assignment)
        {
            try
            {
                _dbContext.Assignments.Add(assignment);
                await _dbContext.SaveChangesAsync();
                return AssignmentMapper.MapToAssignmentFiltered(assignment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Create Assignment");
                throw;
            }


        }

        public async Task<int> DeleteAssignmentAsync(int id)
        {
            var filteredAssignment = _dbContext.Assignments.Where(a => a.AssignmentId == id).FirstOrDefault();
            if (filteredAssignment != null)
            {
                _dbContext.Assignments.Remove(filteredAssignment);
                return await _dbContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<Assignment> GetAssignmentByIdAsync(int id)
        {
            var filteredAssignment = await _dbContext.Assignments.Where(a => a.AssignmentId == id).FirstOrDefaultAsync();
            return filteredAssignment ?? new Assignment();
        }
        public async Task<List<AssignmentRes>> GetAssignmentListAsync()
        {
            var filteredAssignment = await _dbContext.Assignments.ToListAsync();
            return AssignmentMapper.MapToAssignmentFiltered(filteredAssignment) ?? new List<AssignmentRes>();
        }

        public async Task<List<AssignmentRes>> GetAssignmentListByDateAsync()
        {
            var filteredAssignment = await _dbContext.Assignments.OrderBy(a => a.StartDate).ToListAsync();
            return AssignmentMapper.MapToAssignmentFiltered(filteredAssignment) ?? new List<AssignmentRes>();
        }

        public async Task<int> UpdateEndAssignments()
        {
            var oneWeekAgo = DateTime.Now.AddDays(-7);
            var filteredAssignment = await _dbContext.Assignments.Where(a => a.EndDate < oneWeekAgo).ToListAsync();
            foreach (var assignment in filteredAssignment)
            {
                assignment.IsCompleted = true;
            }
            return await _dbContext.SaveChangesAsync();
        }


        public async Task<List<AssignmentRes>> GetAssignmentListCompletedAsync()
        {
            var filteredAssignment = await _dbContext.Assignments.Where(a => a.IsCompleted).ToListAsync();
            return AssignmentMapper.MapToAssignmentFiltered(filteredAssignment) ?? new List<AssignmentRes>();
        }

        public async Task<List<AssignmentType>> GetAssignmentTypeListAsync()
        {
            var filteredAssignmentType = await _dbContext.AssignmentTypes.ToListAsync();
            return filteredAssignmentType ?? new List<AssignmentType>();
        }


        public async Task<int> UpdateAssignmentAsync(Assignment assignment)
        {
            _dbContext.Assignments.Update(assignment);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> UpdateAssignmentAsync<T>(int id, UpdateType updateType, T arg)
        {
            var existingAssignment = await _dbContext.Assignments.FirstOrDefaultAsync(a => a.AssignmentId == id);
            return existingAssignment != null ? await UpdateAssignmentByTypeAsync(existingAssignment, updateType, arg) : 0;
        }

        public async Task<int> UpdateAssignmentsCompletedAsync(string arrAss)
        {
            string[] numberS = arrAss.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
            int[] assignmentIdsToUpdate = Array.ConvertAll(numberS, int.Parse);

            var assignmentsToUpdate = _dbContext.Assignments.Where(a => assignmentIdsToUpdate.Contains(a.AssignmentId));
            foreach (var assignment in assignmentsToUpdate)
            {
                assignment.IsCompleted = true;
            }

            return await _dbContext.SaveChangesAsync();

        }

        /// <summary>
        /// Update assignment by type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="existingAssignment"></param>
        /// <param name="updateType"></param>
        /// <param name="arg"></param>
        /// <exception cref="ArgumentException"></exception>

        private async Task<int> UpdateAssignmentByTypeAsync<T>(Assignment existingAssignment, UpdateType updateType, T arg)
        {
            switch (updateType)
            {
                case UpdateType.Periodic when arg is bool bArg:
                    existingAssignment.IsPeriodic = bArg;
                    break;
                case UpdateType.Completed when arg is bool bArg:
                    existingAssignment.IsCompleted = bArg;
                    break;
                case UpdateType.Archive when arg is bool bArg:
                    existingAssignment.IsArchive = bArg;
                    break;
                case UpdateType.Name when arg is string sArg:
                    existingAssignment.AssignmentName = sArg;
                    break;
                case UpdateType.Description when arg is string sArg:
                    existingAssignment.Description = sArg;
                    break;
                default:
                    throw new ArgumentException("Invalid update type or argument type");
            }
            return await _dbContext.SaveChangesAsync();
        }

    }
}

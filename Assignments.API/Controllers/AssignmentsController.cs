using Assignments.API.BL.CQRS.Command;
using Assignments.API.BL.CQRS.Queries;
using Assignments.API.BL.DTO;
using Assignments.API.BL.Services;
using Assignments.API.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssignmentController : ControllerBase
    {
        private IMediator _mediator;
        public AssignmentController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("UpdateEndAssignments")]
        public async Task<int> UpdateEndAssignments()
        {
            var command = new UpdateEndAssignmentsCommand();
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPost]
        [Route("GetAssignmentsByDate")] 
        public async Task<AssignmentResPagination> GetAssignmentsByDate([FromBody] ReqAssignmentsDto dto)
        {
            var query = new GetAssignmentListByDateQuery();
            var totalResult = await _mediator.Send(query);

            totalResult = AssignmentsTools.GetArchiveResults(totalResult, false);

            if (!String.IsNullOrEmpty(dto.Sort)) 
                totalResult = AssignmentsTools.GetSortResults(totalResult, dto.Sort, dto.Order);

            return AssignmentsTools.GetAssignmentsResPagination(totalResult, dto.Page, dto.PageSize);

        }

        [HttpPost]
        [Route("GetAssignmentsByDateWithArchive")] 
        public async Task<AssignmentResPagination> GetAssignmentsByDateWithArchive([FromBody] ReqAssignmentsDto dto)
        {
            var query = new GetAssignmentListByDateQuery();
            var totalResult = await _mediator.Send(query);

            totalResult = AssignmentsTools.GetArchiveResults(totalResult, true);

            if (!String.IsNullOrEmpty(dto.Sort))
                totalResult = AssignmentsTools.GetSortResults(totalResult, dto.Sort, dto.Order);

            return AssignmentsTools.GetAssignmentsResPagination(totalResult, dto.Page, dto.PageSize);

        }

        [HttpPatch]
        [Route("UpdateAssignmentComplete")]
        public async Task<int> UpdateAssignmentComplete([FromBody] UpdateArrDto dto)
        {
            var command = new UpdateAssignmentCompleteCommand(dto.UpdateArr);
            var result = await _mediator.Send(command);
            return result;
        }
        
        [HttpGet]
        [Route("GetAssignments")]
        public async Task<List<AssignmentRes>> GetAssignments()
        {
            var query = new GetAssignmentListQuery();
            var result = await _mediator.Send(query);
            return result;
        }



        [HttpGet]
        [Route("GetAssignmentsListCompleted")]
        public async Task<List<AssignmentRes>> GetAssignmentsListCompleted()
        {
            var query = new GetAssignmentListCompletedQuery();
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet]
        [Route("GetAssignmentListType")]
        public async Task<List<AssignmentType>> GetAssignmentListType()
        {
            var query = new GetAssignmentTypeListQuery();
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("GetAssignmentById/{id}")]
        public async Task<Assignment> GetAssignmentById(int id)
        {
            var query = new GetAssignmentByIdQuery { AssignmentId = id };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPost("CreateAssignment")]
        public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentItemInputDto dto)
        {
            try
            {
                var command = new CreateAssignmentCommand(AssignmentMapper.MapToCreateAssignment(dto));
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("UpdateAssignment")]
        public async Task<int> UpdateAssignment([FromBody] UpdateAssignmentItemDto dto)
        {
            var command = new UpdateAssignmentCommand(dto);
            var result = await _mediator.Send(command);
            return result;
        }



        [HttpPatch("UpdateAssignmentArchive")]
        public async Task<int> UpdateAssignmentArchive([FromBody] UpdatePatchDto dto)
        {
            var command = new UpdateAssignmentArchiveCommand(dto.Id, dto.Update);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPatch("UpdateAssignmentPeriodic")]
        public async Task<int> UpdateAssignmentPeriodic([FromBody] UpdatePatchDto dto)
        {
            var command = new UpdateAssignmentPeriodicCommand(dto.Id, dto.Update);
            var result = await _mediator.Send(command);
            return result;
        }

        /// <summary>
        /// Only 'moag' Employee can delete assignment:
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize(Policy = "RequireMoagEmp")]
        public async Task<int> DeleteAssignment(int id)
        {
            var command = new DeleteAssignmentCommand() { AssignmentId = id };
            var result = await _mediator.Send(command);
            return result;
        }

    }
}


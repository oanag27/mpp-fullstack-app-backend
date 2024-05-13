using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mmp_prj.Models;
using mmp_prj.Service;

namespace mmp_prj.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SubtaskController : ControllerBase
    {
        private readonly ISubtaskService _subtaskService;

        public SubtaskController(ISubtaskService subtaskService)
        {
            _subtaskService = subtaskService;
        }

        [HttpGet("GetSubtaskById/{id}")]
        public async Task<ActionResult<Subtask>> GetSubtaskById(int id)
        {
            var subtask = await _subtaskService.GetSubtaskByIdAsync(id);
            if (subtask == null)
            {
                return NotFound();
            }
            return subtask;
        }

        [HttpGet("GetAllSubtasks")]
        public async Task<ActionResult<IEnumerable<Subtask>>> GetAllSubtasks()
        {
            var subtasks = await _subtaskService.GetAllSubtasksAsync();
            return Ok(subtasks);
        }

        [HttpPost("AddSubtask")]
        public async Task<IActionResult> AddSubtaskAsync(string name, string description, bool completed, int taskId)
        {
            try
            {
                var subtask = await _subtaskService.AddSubtaskAsync(name, description, completed, taskId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
        
        [HttpGet("CountSubtasksForTask/{taskId}")]
        public async Task<ActionResult<int>> CountSubtasksForTaskAsync(int taskId)
        {
            try
            {
                var subtasks = await _subtaskService.GetAllSubtasksAsync();
                var subtaskCount = subtasks.Count(subtask => subtask.TaskId == taskId);
                return Ok(subtaskCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetAllSubtasksByTaskId/{taskId}")]
        public async Task<ActionResult<IEnumerable<Subtask>>> GetAllSubtasksByTaskId(int taskId)
        {
            try
            {
                var subtasks = await _subtaskService.GetAllSubtasksAsync();

                var subtasksByTaskId = subtasks.Where(s => s.TaskId == taskId).ToList();

                if (subtasksByTaskId == null || !subtasksByTaskId.Any())
                {
                    return NotFound("No subtasks found for the specified task ID.");
                }
                return Ok(subtasksByTaskId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateSubtask/{id}")]
        public async Task<IActionResult> UpdateSubtask(int id, Subtask subtask)
        {
            if (id != subtask.Id)
            {
                return BadRequest();
            }
            var success = await _subtaskService.UpdateSubtaskAsync(id, subtask);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("DeleteSubtask/{id}")]
        public async Task<IActionResult> DeleteSubtask(int id)
        {
            var success = await _subtaskService.DeleteSubtaskAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

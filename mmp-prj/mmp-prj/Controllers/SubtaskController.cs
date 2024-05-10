using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<Subtask>> AddSubtask(Subtask subtask)
        {
            var addedSubtask = await _subtaskService.AddSubtaskAsync(subtask);
            return CreatedAtAction(nameof(GetSubtaskById), new { id = addedSubtask.Id }, addedSubtask);
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

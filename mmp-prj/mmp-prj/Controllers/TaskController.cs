using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using mmp_prj.Models;
using mmp_prj.Service;
using Bogus;

namespace mmp_prj.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TaskController : Controller
{
    private readonly ITaskService taskService;
    private readonly ISubtaskService subtaskService;
    public TaskController(ITaskService _taskService, ISubtaskService subtaskService)
    {
        taskService = _taskService;
        this.subtaskService = subtaskService;
    }
    [HttpPost("AddSubtasksForAllTasks")]
    public async Task<ActionResult> AddSubtasksForAllTasks(int subtaskCount)
    {
        try
        {
            // Get all tasks from the database
            var tasks = await taskService.GetAllTasksAsync();

            if (tasks == null || !tasks.Any())
            {
                return NotFound("No tasks found in the database.");
            }

            // Generate subtasks for each task
            var subtaskFaker = new Faker<Subtask>()
                .RuleFor(s => s.Name, f => f.Random.Word())
                .RuleFor(s => s.Description, f => f.Lorem.Sentence())
                .RuleFor(s => s.Completed, f => f.Random.Bool());

            foreach (var task in tasks)
            {
                var subtasks = subtaskFaker.Generate(subtaskCount);
                foreach (var subtask in subtasks)
                {
                    subtask.TaskId = task.Id;
                   
                }
                foreach (var subtask in subtasks)
                {
                    // Provide values for each parameter when calling AddSubtaskAsync
                    await subtaskService.AddSubtaskAsync(subtask.Name, subtask.Description, (bool)subtask.Completed, (int)subtask.TaskId);
                }
            }

            return Ok($"Added {subtaskCount} subtasks for each task successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("GetTaskById/{id}")]
    public async Task<ActionResult<Models.Task>> GetTaskById(int id)
    {
        var task = await taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return task;
    }

    [HttpGet("GetAllTasks")]
    public async Task<ActionResult<IEnumerable<Models.Task>>> GetAllTasks()
    {
        Console.WriteLine("all");
        var tasks = await taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var success = await taskService.DeleteTaskAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
    [HttpDelete("DeleteName/{name}")]
    public async Task<ActionResult> DeleteName(string name)
    {
        var success = await taskService.DeleteTaskByNameAsync(name);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("AddTask")]
    public async Task<ActionResult<Models.Task>> AddTask(Models.Task task)
    {
        var addedTask = await taskService.AddTaskAsync(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = addedTask.Id }, addedTask);
    }

    [HttpPut("UpdateTask/{id}")]
    public async Task<IActionResult> UpdateTask(int id, Models.Task task)
    {
        if (id != task.Id)
        {
            return BadRequest();
        }
        var success = await taskService.UpdateTaskAsync(id, task);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
    [HttpPut("UpdateTaskByName/{name}")]
    public async Task<IActionResult> UpdateTaskByName(string name, Models.Task task)
    {
        if (name != task.Name)
        {
            return BadRequest();
        }
        var success = await taskService.UpdateTaskByNameAsync(name, task);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
    [HttpGet("GetAllDataSortedByName")]
    public async Task<ActionResult<IEnumerable<Models.Task>>> GetAllDataSortedByName()
    {
        var sortedTasks = await taskService.GetAllTasksSortedByNameAsync();
        return Ok(sortedTasks);
    }
    [HttpPost("PopulateDatabase")]
    public async Task<ActionResult> PopulateDatabase(int numberOfTasks)
    {
        // Create Faker instance for generating task data
        var faker = new Faker<Models.Task>("en")
            .RuleFor(t => t.Name, f => f.Name.FullName())
            .RuleFor(t => t.Description, f => f.Lorem.Sentence(5))
            .RuleFor(t => t.Duration, f => f.Random.Number(1, 100));

        // Generate 150 new tasks
        var tasks = faker.Generate(numberOfTasks);

        // Add tasks to the database
        foreach (var task in tasks)
        {
            await taskService.AddTaskAsync(task);
        }

        // Aggregate subtask counts for each task
        var subtaskCounts = await taskService.CountSubtasksForEachTaskAsync();

        // Return the aggregated data
        return Ok(subtaskCounts);
    }

    [HttpGet("AggregateTaskCounts")]
    public async Task<ActionResult<IEnumerable<TaskCount>>> AggregateTaskCounts()
    {
        // Get all tasks
        var tasks = await taskService.GetAllTasksAsync();

        // Aggregate task counts
        var taskCounts = tasks.GroupBy(t => t.Name)
            .Select(g => new TaskCount
            {
                TaskName = g.Key,
                Count = g.Count()
            })
            .ToList();

        return Ok(taskCounts);
    }
   
    [HttpGet("GetTaskNameById/{taskId}")]
    public async Task<ActionResult<string>> GetTaskNameById(int taskId)
    {
        try
        {
            var taskName = await taskService.GetTaskNameByIdAsync(taskId);
            if (taskName == null)
            {
                return NotFound("No task found with the specified ID.");
            }

            return Ok(taskName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpGet("GetTaskIdByName/{taskName}")]
    public async Task<ActionResult<string>> GetTaskIdByName(string taskName)
    {
        try
        {
            var taskId = await taskService.GetTaskIdByNameAsync(taskName);
            if (taskId == null)
            {
                return NotFound("No task found with the specified name.");
            }

            return Ok(taskId);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    public class TaskCount
{
    public string TaskName { get; set; }
    public int Count { get; set; }
}
}

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using mmp_prj.Models;
using mmp_prj.Service;

namespace mmp_prj.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TaskController : Controller
{
    private readonly ITaskService taskService;
    public TaskController(ITaskService _taskService)
    {
        taskService = _taskService;
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
        var tasks = Enumerable.Range(1, numberOfTasks)
            .Select(i => new Models.Task
            {
                Name = Faker.Name.FullName(),
                Description = Faker.Lorem.Sentence(5),
                Duration = Faker.RandomNumber.Next(1, 100)
            });

        // Add tasks to the database
        foreach (var task in tasks)
        {
            await taskService.AddTaskAsync(task);
        }

        return Ok("Database populated successfully.");
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


public class TaskCount
{
    public string TaskName { get; set; }
    public int Count { get; set; }
}
}

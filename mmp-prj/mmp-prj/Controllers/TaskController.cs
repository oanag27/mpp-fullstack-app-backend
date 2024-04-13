using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using mmp_prj.Models;
namespace mmp_prj.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TaskController : Controller
{
    //public static List<Tasks> tasks = new List<Tasks>()
    //{
    //    new Tasks { Id = 1, Name = "Task 1", Description = "Description for Task 1", Duration = 10 },
    //    new Tasks { Id = 2, Name = "Task 2", Description = "Description for Task 2", Duration = 20 },
    //    new Tasks { Id = 3, Name = "Task 3", Description = "Description for Task 3", Duration = 7 },
    //    new Tasks { Id = 4, Name = "Task 4", Description = "Description for Task 4", Duration = 100 },
    //    new Tasks { Id = 5, Name = "Task 5", Description = "Description for Task 5", Duration = 50 }
    //};
    public static List<Tasks> tasks = new List<Tasks>();
    public TaskController()
    {
        // Generate fake tasks using Faker
        for (int i = 0; i < 5; i++)
        {
            tasks.Add(new Tasks
            {
                Id = i + 1,
                Name = Faker.Name.FullName(),
                Description = Faker.Lorem.Sentence(5),
                Duration = Faker.RandomNumber.Next(1, 100)
            });
        }
    }

    [HttpGet("GetTaskById/{id}")]
    public virtual ActionResult<Tasks> GetTaskById(int id)
    {
        var task = tasks.Find(t => t.Id == id);
        if (task == null)
        {
            return NotFound();
        }
        return task;
    }

    [HttpGet("GetAllTasks")]
    public virtual ActionResult<IEnumerable<Tasks>> GetAllTasks()
    {
        return Ok(tasks);
    }

    [HttpDelete("Delete/{id}")]
    public virtual ActionResult Delete(int id)
    {
        var task = tasks.Find(t => t.Id == id);
        if (task == null)
        {
            return NotFound();
        }
        tasks.Remove(task);
        return NoContent();
    }

    [HttpPost("AddTask")]
    public virtual ActionResult<Tasks> AddTask(Tasks task)
    {
        // Find the maximum id in the existing tasks list
        int maxId = tasks.Max(t => t.Id);

        // Set the id of the new task to be one greater than the maximum id
        task.Id = maxId + 1;

        tasks.Add(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    [HttpPut("UpdateTask/{id}")]
    public virtual IActionResult UpdateTask(int id, Tasks task)
    {
        if (id != task.Id)
        {
            return BadRequest();
        }
        var existingTask = tasks.Find(t => t.Id == id);
        if (existingTask == null)
        {
            return NotFound();
        }
        existingTask.Name = task.Name;
        existingTask.Description = task.Description;
        existingTask.Duration = task.Duration;
        return NoContent();
    }

    [HttpGet("GetAllDataSortedByName")]
    public virtual ActionResult<IEnumerable<Tasks>> GetAllDataSortedByName()
    {
        // Sort tasks by name
        IEnumerable<Tasks> sortedTasks = tasks.OrderBy(t => t.Name);
        return Ok(sortedTasks);
    }

}

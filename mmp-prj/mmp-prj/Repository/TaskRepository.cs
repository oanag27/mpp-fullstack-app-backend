using Microsoft.EntityFrameworkCore;
using mmp_prj.Models;

namespace mmp_prj.Repository
{
    public class TaskRepository : ITaskRepository
    {
        //private readonly List<Tasks> _tasks;
        private readonly MppContext context;
        public TaskRepository(MppContext _context)
        {
            this.context = _context;
        }
        public async Task<Models.Task> AddTaskAsync(Models.Task task)
        {
            context.Tasks.Add(task);
            await context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task != null)
            {
                context.Tasks.Remove(task);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTaskByNameAsync(string name)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(t => t.Name == name);
            if (task != null)
            {
                context.Tasks.Remove(task);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Models.Task>> GetAllTasksAsync()
        {
            return await context.Tasks.ToListAsync();
        }

        public async Task<IEnumerable<Models.Task>> GetAllTasksSortedByNameAsync()
        {
            return await context.Tasks.OrderBy(t => t.Name).ToListAsync();
        }

        public async Task<Models.Task> GetTaskByIdAsync(int id)
        {
            return await context.Tasks.FindAsync(id);
        }
        
        public async Task<bool> UpdateTaskAsync(int id, Models.Task task)
        {
            var existingTask = await context.Tasks.FindAsync(id);
            if (existingTask != null)
            {
                existingTask.Name = task.Name;
                existingTask.Description = task.Description;
                existingTask.Duration = task.Duration;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateTaskByNameAsync(string name, Models.Task task)
        {
            var existingTask = await context.Tasks.FirstOrDefaultAsync(t => t.Name == name);
            if (existingTask != null)
            {
                existingTask.Name = task.Name;
                existingTask.Description = task.Description;
                existingTask.Duration = task.Duration;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

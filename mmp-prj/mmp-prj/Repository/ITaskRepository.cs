using System.Collections;
using mmp_prj.Models;

namespace mmp_prj.Repository
{
    public interface ITaskRepository
    {
        Task<Models.Task> GetTaskByIdAsync(int id);
        Task<IEnumerable<Models.Task>> GetAllTasksAsync();
        Task<Models.Task> AddTaskAsync(Models.Task task);
        Task<bool> DeleteTaskAsync(int id);
        Task<bool> DeleteTaskByNameAsync(string name);
        Task<bool> UpdateTaskAsync(int id, Models.Task task);
        Task<bool> UpdateTaskByNameAsync(string name, Models.Task task);
        Task<IEnumerable<Models.Task>> GetAllTasksSortedByNameAsync();
        Task<Dictionary<int,int>> CountSubtasksForEachTaskAsync();
        Task<string> GetTaskNameByIdAsync(int taskId);
        Task<int> GetTaskIdByNameAsync(string taskName);
    }
}

using mmp_prj.Models;
using System.Threading.Tasks;

namespace mmp_prj.Service
{
    public interface ITaskService
    {
        public Task<Models.Task> GetTaskByIdAsync(int id);
        public Task<IEnumerable<Models.Task>> GetAllTasksAsync();
        public Task<Models.Task> AddTaskAsync(Models.Task task);
        public Task<bool> DeleteTaskAsync(int id);
        public Task<bool> DeleteTaskByNameAsync(string name);
        public Task<bool> UpdateTaskAsync(int id, Models.Task task);
        public Task<bool> UpdateTaskByNameAsync(string name, Models.Task task);
        public Task<IEnumerable<Models.Task>> GetAllTasksSortedByNameAsync();
        public Task<Dictionary<int, int>> CountSubtasksForEachTaskAsync();
        public Task<string> GetTaskNameByIdAsync(int taskId);
        public Task<int> GetTaskIdByNameAsync(string taskName);
    }
}

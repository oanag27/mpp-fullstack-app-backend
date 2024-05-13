using mmp_prj.Models;
using mmp_prj.Repository;

namespace mmp_prj.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<Models.Task> AddTaskAsync(Models.Task task)
        {
            return await _taskRepository.AddTaskAsync(task);
        }

        public async Task<Dictionary<int, int>> CountSubtasksForEachTaskAsync()
        {
            return await _taskRepository.CountSubtasksForEachTaskAsync();
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _taskRepository.DeleteTaskAsync(id);
        }

        public async Task<bool> DeleteTaskByNameAsync(string name)
        {
            return await _taskRepository.DeleteTaskByNameAsync(name);
        }

        public async Task<IEnumerable<Models.Task>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllTasksAsync();
        }

        public async Task<IEnumerable<Models.Task>> GetAllTasksSortedByNameAsync()
        {
            return await _taskRepository.GetAllTasksSortedByNameAsync();
        }

        public async Task<Models.Task> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetTaskByIdAsync(id);
        }

        public async Task<int> GetTaskIdByNameAsync(string taskName)
        {
            return await _taskRepository.GetTaskIdByNameAsync(taskName);
        }

        public async Task<string> GetTaskNameByIdAsync(int taskId)
        {
            return await _taskRepository.GetTaskNameByIdAsync(taskId);
        }

        public async Task<bool> UpdateTaskAsync(int id, Models.Task task)
        {
            return await _taskRepository.UpdateTaskAsync(id, task);
        }

        public async Task<bool> UpdateTaskByNameAsync(string name, Models.Task task)
        {
            return await _taskRepository.UpdateTaskByNameAsync(name, task);
        }
    }
}

using mmp_prj.Models;
using mmp_prj.Repository;

namespace mmp_prj.Service
{
    public class SubtaskService:ISubtaskService
    {
        private readonly ISubtaskRepository _subtaskRepository;

        public SubtaskService(ISubtaskRepository subtaskRepository)
        {
            _subtaskRepository = subtaskRepository;
        }

        public Task<Subtask> GetSubtaskByIdAsync(int id)
        {
            return _subtaskRepository.GetSubtaskByIdAsync(id);
        }

        public Task<IEnumerable<Subtask>> GetAllSubtasksAsync()
        {
            return _subtaskRepository.GetAllSubtasksAsync();
        }

        public Task<Subtask> AddSubtaskAsync(string name, string description, bool completed, int taskId)
        {
            return _subtaskRepository.AddSubtaskAsync(name, description, completed, taskId);
        }
        public Task<bool> DeleteSubtaskAsync(int id)
        {
            return _subtaskRepository.DeleteSubtaskAsync(id);
        }

        public Task<bool> UpdateSubtaskAsync(int id, Subtask subtask)
        {
            return _subtaskRepository.UpdateSubtaskAsync(id, subtask);
        }

    }
}

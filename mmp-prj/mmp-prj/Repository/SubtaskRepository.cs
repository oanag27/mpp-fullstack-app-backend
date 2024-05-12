using Microsoft.EntityFrameworkCore;
using mmp_prj.Models;

namespace mmp_prj.Repository
{
    public class SubtaskRepository : ISubtaskRepository
    {
        private readonly MppContext _context;
        public SubtaskRepository(MppContext context)
        {
            _context = context;
        }
        //public async Task<Subtask> AddSubtaskAsync(Subtask subtask)
        //{
        //    _context.Subtasks.Add(subtask);
        //    await _context.SaveChangesAsync();
        //    return subtask;
        //}
        public async Task<Subtask> AddSubtaskAsync(string name, string description, bool completed, int taskId)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("Name and description are required.");
            }
            var subtask = new Subtask
            {
                Name = name,
                Description = description,
                Completed = completed,
                TaskId = taskId
            };
            _context.Subtasks.Add(subtask);
            await _context.SaveChangesAsync();
            return subtask;
        }

        public async Task<bool> DeleteSubtaskAsync(int id)
        {
            var subtask = await _context.Subtasks.FindAsync(id);
            if (subtask == null)
                return false;

            _context.Subtasks.Remove(subtask);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateSubtaskAsync(int id, Subtask subtask)
        {
            if (id != subtask.Id)
                return false;

            _context.Entry(subtask).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Subtask> GetSubtaskByIdAsync(int id)
        {
            return await _context.Subtasks.FindAsync(id);
        }

        public async Task<IEnumerable<Subtask>> GetAllSubtasksAsync()
        {
            return _context.Subtasks.ToList();
        }
        public class SubtaskInputModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Completed { get; set; }
            public int TaskId { get; set; }
        }
    }
    

}

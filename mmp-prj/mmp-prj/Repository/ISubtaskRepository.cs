﻿using mmp_prj.Models;

namespace mmp_prj.Repository
{
    public interface ISubtaskRepository
    {
        Task<Subtask> GetSubtaskByIdAsync(int id);
        Task<IEnumerable<Subtask>> GetAllSubtasksAsync();
        Task<Subtask> AddSubtaskAsync(Subtask subtask);
        Task<bool> DeleteSubtaskAsync(int id);
        Task<bool> UpdateSubtaskAsync(int id, Subtask subtask);
    }
}
﻿using RamsTrackerAPI.Models.Domain;

namespace RamsTrackerAPI.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(Guid id);

        Task<Project> CreateAsync(Project project);

        Task<Project?> UpdateAsync(Guid id, Project project);
        Task<Project?> DeleteAsync(Guid id);
    }
}

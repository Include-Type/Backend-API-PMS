using System.Collections.Generic;
using System.Threading.Tasks;
using IncludeTypeBackend.Dtos;
using IncludeTypeBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace IncludeTypeBackend.Services
{
    public class ProjectService
    {
        private readonly PostgreSqlContext _db;

        public ProjectService(PostgreSqlContext db) => _db = db;

        public async Task<List<ProjectTask>> GetAllTasksAsync() => await _db.ProjectTask.ToListAsync();

        public async Task<int> GetTotalTasksAsync()
        {
            List<ProjectTask> projectTasks = await GetAllTasksAsync();
            return projectTasks.Count;
        }
    }
}
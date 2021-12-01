namespace IncludeTypeBackend.Services;

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

    public async Task<List<ProjectTask>> GetAllTasksByUsernameAsync(string username) 
    {
        return await _db.ProjectTask.Where(task => task.Assigned.Contains(username)).ToListAsync();
    }

    public async Task UpdateAllTasksByUsernameAsync(ProjectTask[] projectTasks, string username)
    {
        foreach (ProjectTask projectTask in _db.ProjectTask)
        {
            if (projectTask.Assigned.Contains(username))
            {
                _db.ProjectTask.Remove(projectTask);
            }
        }

        await _db.ProjectTask.AddRangeAsync(projectTasks);
        await _db.SaveChangesAsync();
    }

    public async Task<List<ProjectTask>> GetAllTasksForGivenDeadlineAsync(string key)
    {
        return await _db.ProjectTask.Where(task => task.Deadline == key).ToListAsync();
    }
}

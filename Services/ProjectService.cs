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

    public async Task UpdateAllTasksAsync(ProjectTask[] projectTasks, string username)
    {
        List<ProjectTask> ptask = await GetAllTasksByUsernameAsync(username);
        foreach(ProjectTask p in ptask)
        {
            _db.ProjectTask.Remove(p);
        }
        await _db.ProjectTask.AddRangeAsync(projectTasks);
        await _db.SaveChangesAsync(); // Delete this temporary line first
    }
}

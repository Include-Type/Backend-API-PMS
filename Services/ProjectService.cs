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

    public async Task<List<ProjectTask>> GetAllTasksByAuthorAsync(string author)
    {
        return await _db.ProjectTask.Where(task => task.Author.Equals(author)).ToListAsync();
    }

    public async Task<List<ProjectTask>> GetAllTasksByUsernameAsync(string username)
    {
        return await _db.ProjectTask.Where(task => task.Assigned.Contains(username)).ToListAsync();
    }

    public async Task UpdateAllTasksByAuthorAsync(ProjectTask[] projectTasks, string author)
    {
        foreach (ProjectTask projectTask in _db.ProjectTask)
        {
            if (projectTask.Author.Equals(author))
            {
                _db.ProjectTask.Remove(projectTask);
            }
        }

        foreach (ProjectTask projectTask in projectTasks)
        {
            if (projectTask.Id.Length < 10)
            {
                Guid guid = Guid.NewGuid();
                projectTask.Id = Convert.ToString(guid);
            }

            await _db.ProjectTask.AddAsync(projectTask);
        }

        await _db.SaveChangesAsync();
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

        foreach (ProjectTask projectTask in projectTasks)
        {
            if (projectTask.Id.Length < 10)
            {
                Guid guid = Guid.NewGuid();
                projectTask.Id = Convert.ToString(guid);
            }

            await _db.ProjectTask.AddAsync(projectTask);
        }

        await _db.SaveChangesAsync();
    }

    public async Task<List<ProjectTask>> GetAllTasksForGivenDeadlineAsync(string key)
    {
        return await _db.ProjectTask.Where(task => task.Deadline == key).ToListAsync();
    }
}

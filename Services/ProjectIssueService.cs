namespace IncludeTypeBackend.Services;

public class ProjectIssueService
{
    private readonly PostgreSqlContext _db;

    public ProjectIssueService(PostgreSqlContext db) => _db = db;

    public async Task<List<ProjectIssue>> GetAllIssuesAsync() => await _db.ProjectIssue.ToListAsync();

    public async Task<int> GetTotalIssuesAsync()
    {
        List<ProjectIssue> projectIssue = await GetAllIssuesAsync();
        return projectIssue.Count;
    }

    public async Task<List<ProjectIssue>> GetAllIssuesByAuthorAsync(string author)
    {
        return await _db.ProjectIssue.Where(issue => issue.Author.Equals(author)).ToListAsync();
    }

    public async Task<List<ProjectIssue>> GetAllIssuesByUsernameAsync(string username)
    {
        return await _db.ProjectIssue.Where(issue => issue.Assigned.Contains(username)).ToListAsync();
    }

    public async Task UpdateAllIssuesByAuthorAsync(ProjectIssue[] projectIssues, string author)
    {
        foreach (ProjectIssue projectIssue in _db.ProjectIssue)
        {
            if (projectIssue.Author.Equals(author))
            {
                _db.ProjectIssue.Remove(projectIssue);
            }
        }

        foreach (ProjectIssue projectIssue in projectIssues)
        {
            if (projectIssue.Id.Length < 10)
            {
                Guid guid = Guid.NewGuid();
                projectIssue.Id = Convert.ToString(guid);
            }

            await _db.ProjectIssue.AddAsync(projectIssue);
        }

        await _db.SaveChangesAsync();
    }

    public async Task UpdateAllIssuesByUsernameAsync(ProjectIssue[] projectIssues, string username)
    {
        foreach (ProjectIssue projectIssue in _db.ProjectIssue)
        {
            if (projectIssue.Assigned.Contains(username))
            {
                _db.ProjectIssue.Remove(projectIssue);
            }
        }

        foreach (ProjectIssue projectIssue in projectIssues)
        {
            if (projectIssue.Id.Length < 10)
            {
                Guid guid = Guid.NewGuid();
                projectIssue.Id = Convert.ToString(guid);
            }

            await _db.ProjectIssue.AddAsync(projectIssue);
        }

        await _db.SaveChangesAsync();
    }

    public async Task<List<ProjectIssue>> GetAllIssuesForGivenDeadlineAsync(string key)
    {
        return await _db.ProjectIssue.Where(issue => issue.Deadline.Equals(key)).ToListAsync();
    }

    public async Task AddIssueAsync(ProjectIssue issue)
    {
        await _db.ProjectIssue.AddAsync(issue);
        await _db.SaveChangesAsync();
    }
}

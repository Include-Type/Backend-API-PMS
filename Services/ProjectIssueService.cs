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

    public async Task<List<ProjectIssue>> GetAllIssueByAuthorAsync(string author)
    {
        return await _db.ProjectIssue.Where(issue => issue.Author.Equals(author)).ToListAsync();
    }

    public async Task<List<ProjectIssue>> GetAllIssueByUsernameAsync(string username)
    {
        return await _db.ProjectIssue.Where(issue => issue.Assigned.Contains(username)).ToListAsync();
    }

     public async Task UpdateAllIssueByAuthorAsync(ProjectIssue[] projectIssue, string author)
    {
        foreach (ProjectIssue projectIssues in _db.ProjectIssue)
        {
            if (projectIssues.Author.Equals(author))
            {
                _db.ProjectIssue.Remove(projectIssues);
            }
        }

        foreach (ProjectIssue projectIssues in projectIssue)
        {
            if (projectIssues.Id.Length < 10)
            {
                Guid guid = Guid.NewGuid();
                projectIssues.Id = Convert.ToString(guid);
            }

            await _db.ProjectIssue.AddAsync(projectIssues);
        }

        await _db.SaveChangesAsync();
    }

    public async Task UpdateAllIssueByUsernameAsync(ProjectIssue[] projectIssues, string username)
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


}

namespace IncludeTypeBackend.Services;

public class ProjectIssueService
{
    private readonly PostgreSqlContext _db;

    public ProjectIssueService(PostgreSqlContext db) => _db = db;
}

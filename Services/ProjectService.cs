namespace IncludeTypeBackend.Services;

public class ProjectService
{
    private readonly PostgreSqlContext _db;

    public ProjectService(PostgreSqlContext db) => _db = db;

    public async Task<List<Project>> GetAllProjectsAsync() => await _db.Project.ToListAsync();

    public async Task<int> GetTotalProjectsAsync()
    {
        List<Project> projects = await GetAllProjectsAsync();
        return projects.Count;
    }

    public async Task<List<Project>> GetAllProjectsByUsernameAsync(string username)
    {
        List<ProjectMember> projectNames = await _db.ProjectMember
                                                    .Where(member => member.Username.Equals(username))
                                                    .ToListAsync();
        List<Project> result = new();
        foreach (ProjectMember member in projectNames)
        {
            Project project = await _db.Project.FirstOrDefaultAsync(p => p.Name.Equals(member.ProjName));
            if (project is not null)
            {
                result.Add(project);
            }
        }

        return result;
    }

    public async Task<Project> GetProjectAsync(string projectName) =>
        await _db.Project.FirstOrDefaultAsync(p => p.Name.Equals(projectName));

    public async Task<List<ProjectMember>> GetAllProjectMembersAsync(string projectName) =>
        await _db.ProjectMember
                 .Where(m => m.ProjName.Equals(projectName))
                 .ToListAsync();

    public async Task<bool> CheckForAdminAsync(string projectName, string username)
    {
        ProjectMember member = await _db.ProjectMember.FirstOrDefaultAsync(m =>
            m.ProjName.Equals(projectName) &&
            m.Username.Equals(username) &&
            m.Role.Equals("Admin")
        );

        if (member is not null)
        {
            return true;
        }

        return false;
    }

    public async Task<ProjectDetailsDto> GetProjectDetailsAsync(string projectName, string username)
    {
        Project project = await GetProjectAsync(projectName);
        List<ProjectMember> projectMembers = await GetAllProjectMembersAsync(projectName);
        ProjectMember adminMember = projectMembers.FirstOrDefault(m =>
            m.Username.Equals(username) &&
            m.Role.Equals("Admin")
        );

        bool isAdmin;
        if (adminMember is not null)
        {
            isAdmin = true;
        }
        else
        {
            isAdmin = false;
        }

        return new ProjectDetailsDto()
        {
            Project = project,
            ProjectMembers = projectMembers,
            IsAdmin = isAdmin
        };
    }
}

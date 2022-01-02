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

    public async Task<Project> GetProjectAsync(string key) =>
        await _db.Project.FirstOrDefaultAsync(p => p.Id.Equals(key) || p.Name.Equals(key));

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

    public async Task AddProjectAsync(Project project, string adminUsername)
    {
        await _db.Project.AddAsync(project);
        User user = await _db.User.FirstOrDefaultAsync(u => u.Username.Equals(adminUsername));
        ProjectMember newAdminUser = new()
        {
            Id = Convert.ToString(Guid.NewGuid()),
            ProjName = project.Name,
            Name = $"{user.FirstName} {user.LastName}",
            Role = "Admin",
            Username = user.Username
        };
        await _db.ProjectMember.AddAsync(newAdminUser);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateProjectAsync(Project existingProject, Project updatedProject)
    {
        if (!existingProject.Name.Equals(updatedProject.Name))
        {
            List<ProjectMember> projectMembers = await GetAllProjectMembersAsync(existingProject.Name);
            foreach (ProjectMember projectMember in projectMembers)
            {
                projectMember.ProjName = updatedProject.Name;
            }
        }

        existingProject.Id = updatedProject.Id;
        existingProject.Date = updatedProject.Date;
        existingProject.Name = updatedProject.Name;
        existingProject.Status = updatedProject.Status;
        existingProject.About = updatedProject.About;
        existingProject.Documentation = updatedProject.Documentation;

        await _db.SaveChangesAsync();
    }

    public async Task UpdateProjectMembersAsync(string projectName, List<ProjectMember> projectMembers)
    {
        _db.ProjectMember.RemoveRange(await GetAllProjectMembersAsync(projectName));

        foreach (ProjectMember projectMember in projectMembers)
        {
            if (projectMember.Id.Length < 10)
            {
                Guid guid = Guid.NewGuid();
                projectMember.Id = Convert.ToString(guid);
            }

            await _db.ProjectMember.AddAsync(projectMember);
        }

        await _db.SaveChangesAsync();
    }

    public async Task DeleteAllTerminatedProjectsAsync()
    {
        List<Project> terminatedProjects = await _db.Project
                                                    .Where(p => p.Status.Equals("Terminated"))
                                                    .ToListAsync();
        foreach (Project project in terminatedProjects)
        {
            _db.ProjectMember.RemoveRange(await GetAllProjectMembersAsync(project.Name));
        }

        _db.Project.RemoveRange(terminatedProjects);
        await _db.SaveChangesAsync();
    }
}

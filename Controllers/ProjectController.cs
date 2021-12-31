namespace IncludeTypeBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ProjectService _project;

    public ProjectController(ProjectService project) => _project = project;

    [HttpGet("[action]")]
    public async Task<ActionResult<List<Project>>> GetAllProjects() => await _project.GetAllProjectsAsync();

    [HttpGet("[action]")]
    public async Task<ActionResult<int>> GetTotalProjects() => await _project.GetTotalProjectsAsync();

    [HttpGet("[action]/{username}")]
    public async Task<ActionResult<List<Project>>> GetAllProjectsByUsername(string username)
    {
        List<Project> projects = await _project.GetAllProjectsByUsernameAsync(username);
        return projects;
    }

    [HttpGet("[action]/{projectWithUsername}")]
    public async Task<ActionResult<ProjectDetailsDto>> GetProjectDetails(string projectWithUsername)
    {
        string[] temp = projectWithUsername.Split('&');

        Project project = await _project.GetProjectAsync(temp[0]);
        if (project is null)
        {
            return NotFound("Project not found");
        }

        return await _project.GetProjectDetailsAsync(temp[0], temp[1]);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> AddProject([FromBody] Project project)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid request");
        }

        Guid guid = Guid.NewGuid();
        project.Id = Convert.ToString(guid);
        await _project.AddProjectAsync(project);
        return Ok("Project successfully added.");
    }

    [HttpPost("[action]/{projName}")]
    public async Task<ActionResult> UpdateProject(string projName, [FromBody] Project updatedProject)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid request");
        }

        Project existingProject = await _project.GetProjectAsync(projName);
        if (existingProject is null)
        {
            return NotFound("Project not found");
        }

        await _project.UpdateProjectAsync(existingProject, updatedProject);
        return Ok("Project successfully updated.");
    }

    [HttpPost("[action]/{projName}")]
    public async Task<ActionResult> UpdateProjectMembers(string projName, [FromBody] List<ProjectMember> projectMembers)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid request");
        }

        Project project = await _project.GetProjectAsync(projName);
        if (project is null)
        {
            return NotFound("Project not found");
        }

        await _project.UpdateProjectMembersAsync(projName, projectMembers);
        return Ok("Project members updated.");
    }
}

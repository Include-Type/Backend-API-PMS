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
        return await _project.GetProjectDetailsAsync(temp[0], temp[1]);
    }
}

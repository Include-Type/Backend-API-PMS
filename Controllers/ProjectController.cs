namespace IncludeTypeBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ProjectService _project;

    public ProjectController(ProjectService project) => _project = project;

    [HttpGet("[action]")]
    public async Task<ActionResult<List<ProjectTask>>> GetTasks() => await _project.GetAllTasksAsync();

    [HttpGet("[action]")]
    public async Task<ActionResult<int>> GetTotalTasks() => await _project.GetTotalTasksAsync();

    [HttpGet("[action]/{username}")]
    public async Task<ActionResult<List<ProjectTask>>> GetTasksByUsername(string username)
    {
        List<ProjectTask> projectTasks = await _project.GetAllTasksByUsernameAsync(username);
        projectTasks.Sort(new ProjectTaskComparer());
        return projectTasks;
    }

    [HttpPost("[action]/{username}")]
    public async Task<ActionResult> UpdateTasks([FromBody] ProjectTaskDto projectTasks, string username)
    {
        if (ModelState.IsValid)
        {
            await _project.UpdateAllTasksAsync(projectTasks.Tasks, username);
            return Ok("Project tasks updated.");
        }

        return BadRequest("Invalid user credentials!");
    }
}

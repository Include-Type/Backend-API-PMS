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

    [HttpGet("[action]/{author}")]
    public async Task<ActionResult<List<ProjectTask>>> GetTasksByAuthor(string author)
    {
        List<ProjectTask> projectTasks = await _project.GetAllTasksByAuthorAsync(author);
        projectTasks.Sort(new ProjectTaskComparer());
        return projectTasks;
    }

    [HttpGet("[action]/{username}")]
    public async Task<ActionResult<List<ProjectTask>>> GetTasksByUsername(string username)
    {
        List<ProjectTask> projectTasks = await _project.GetAllTasksByUsernameAsync(username);
        projectTasks.Sort(new ProjectTaskComparer());
        return projectTasks;
    }

    [HttpPost("[action]/{author}")]
    public async Task<ActionResult> UpdateTasksByAuthor([FromBody] ProjectTaskDto projectTasks, string author)
    {
        if (ModelState.IsValid)
        {
            await _project.UpdateAllTasksByAuthorAsync(projectTasks.Tasks, author);
            return Ok("Project tasks updated.");
        }

        return BadRequest("Invalid user credentials!");
    }

    [HttpPost("[action]/{username}")]
    public async Task<ActionResult> UpdateTasksByUsername([FromBody] ProjectTaskDto projectTasks, string username)
    {
        if (ModelState.IsValid)
        {
            await _project.UpdateAllTasksByUsernameAsync(projectTasks.Tasks, username);
            return Ok("Project tasks updated.");
        }

        return BadRequest("Invalid user credentials!");
    }

    [HttpGet("[action]/{key}")]
    public async Task<ActionResult<List<ProjectTask>>> GetTasksForGivenDeadline(string key)
    {
        List<ProjectTask> projectTasks = await _project.GetAllTasksForGivenDeadlineAsync(key);
        projectTasks.Sort(new ProjectTaskComparer());
        return projectTasks;
    }
}

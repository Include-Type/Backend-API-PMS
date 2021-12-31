namespace IncludeTypeBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectIssueController : ControllerBase
{
    private readonly ProjectIssueService _project;

    public ProjectIssueController(ProjectIssueService project) => _project = project;

    [HttpGet("[action]")]
    public async Task<ActionResult<List<ProjectIssue>>> GetIssues() => await _project.GetAllIssuesAsync();

    [HttpGet("[action]")]
    public async Task<ActionResult<int>> GetTotalIssues() => await _project.GetTotalIssuesAsync();

    [HttpGet("[action]/{author}")]
    public async Task<ActionResult<List<ProjectIssue>>> GetIssuesByAuthor(string author)
    {
        List<ProjectIssue> projectIssues = await _project.GetAllIssuesByAuthorAsync(author);
        projectIssues.Sort(new ProjectIssueComparer());
        return projectIssues;
    }

    [HttpGet("[action]/{username}")]
    public async Task<ActionResult<List<ProjectIssue>>> GetIssuesByUsername(string username)
    {
        List<ProjectIssue> projectIssues = await _project.GetAllIssuesByUsernameAsync(username);
        projectIssues.Sort(new ProjectIssueComparer());
        return projectIssues;
    }

    [HttpPost("[action]/{author}")]
    public async Task<ActionResult> UpdateIssuesByAuthor([FromBody] ProjectIssueDto projectIssues, string author)
    {
        if (ModelState.IsValid)
        {
            await _project.UpdateAllIssuesByAuthorAsync(projectIssues.Issues, author);
            return Ok("Project issues updated.");
        }

        return BadRequest("Invalid user credentials!");
    }

    [HttpPost("[action]/{username}")]
    public async Task<ActionResult> UpdateIssuesByUsername([FromBody] ProjectIssueDto projectIssues, string username)
    {
        if (ModelState.IsValid)
        {
            await _project.UpdateAllIssuesByUsernameAsync(projectIssues.Issues, username);
            return Ok("Project issues updated.");
        }

        return BadRequest("Invalid user credentials!");
    }

    [HttpGet("[action]/{key}")]
    public async Task<ActionResult<List<ProjectIssue>>> GetIssuesForGivenDeadline(string key)
    {
        List<ProjectIssue> projectIssues = await _project.GetAllIssuesForGivenDeadlineAsync(key);
        projectIssues.Sort(new ProjectIssueComparer());
        return projectIssues;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> AddIssue([FromBody] ProjectIssue issue)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid request");
        }

        Guid guid = Guid.NewGuid();
        issue.Id = Convert.ToString(guid);
        await _project.AddIssueAsync(issue);
        return Ok("Issue successfully added.");
    }
}

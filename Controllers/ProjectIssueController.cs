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
        List<ProjectIssue> projectIssue = await _project.GetAllIssueByAuthorAsync(author);
        return projectIssue;
    }

    [HttpGet("[action]/{username}")]
    public async Task<ActionResult<List<ProjectIssue>>> GetIssueByUsername(string username)
    {
        List<ProjectIssue> projectIssue = await _project.GetAllIssueByUsernameAsync(username);
        return projectIssue;
    }

     [HttpPost("[action]/{author}")]
    public async Task<ActionResult> UpdateIssueByAuthor([FromBody] ProjectIssueDto projectIssue, string author)
    {
        if (ModelState.IsValid)
        {
            await _project.UpdateAllIssueByAuthorAsync(projectIssue.Issues, author);
            return Ok("Project issue updated.");
        }

        return BadRequest("Invalid user credentials!");
    }

    [HttpPost("[action]/{username}")]
    public async Task<ActionResult> UpdateIssueByUsername([FromBody] ProjectIssueDto projectIssue, string username)
    {
        if (ModelState.IsValid)
        {
            await _project.UpdateAllIssueByUsernameAsync(projectIssue.Issues, username);
            return Ok("Project issue updated.");
        }

        return BadRequest("Invalid user credentials!");
    }


}



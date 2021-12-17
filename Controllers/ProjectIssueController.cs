namespace IncludeTypeBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectIssueController : ControllerBase
{
    private readonly ProjectIssueService _project;

    public ProjectIssueController(ProjectIssueService project) => _project = project;
}

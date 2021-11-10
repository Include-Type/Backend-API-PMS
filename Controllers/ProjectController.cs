namespace IncludeTypeBackend.Controllers
{
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
    }
}
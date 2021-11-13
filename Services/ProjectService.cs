namespace IncludeTypeBackend.Services;

public class ProjectService
{
    private readonly PostgreSqlContext _db;

    public ProjectService(PostgreSqlContext db) => _db = db;

    public async Task<List<ProjectTask>> GetAllTasksAsync() => await _db.ProjectTask.ToListAsync();

    public async Task<int> GetTotalTasksAsync()
    {
        List<ProjectTask> projectTasks = await GetAllTasksAsync();
        return projectTasks.Count;
    }

    public async Task<List<ProjectTask>> GetAllTasksByUsernameAsync(string username)
    {
        /*
        Method description:
            Returns: A list of all tasks where `username` is present 
            in the `Assigned` property of a ProjectTask.
        
        Eg:
            There's a task T1 where T1.Assigned = "rohanhalder + subhamk108"
            So if username = "subhamk108" is passed to this method as parameter, 
            it should return a list having T1 and all such tasks.

        Info:
            `_db` is the DB.
            All tasks in the DB are accessed by the `_db.ProjectTask` property.
        */
        // Implement this method.
        return await GetAllTasksAsync(); // Delete this temporary line first
    }

    public async Task UpdateAllTasksAsync(ProjectTask[] projectTasks, string username)
    {
        /*
        Method description:
        Returns: Void / Nothing
        This method will update all tasks based on the `username`

        Algorithm:
            - Delete all tasks in the DB whose `Assigned` property contains `username`.
            - Add all tasks from the `projectTasks` (array) method parameter to the DB.
            - Save the DB using the `SaveChangesAsync()` function.

        Info:
            `_db` is the DB.
            All tasks in the DB are accessed by the `_db.ProjectTask` property.
        */
        // Implement this method.
        await _db.SaveChangesAsync(); // Delete this temporary line first
    }
}

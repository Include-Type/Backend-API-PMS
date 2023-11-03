namespace IncludeTypeBackend.Models;

public class ProjectMember
{
    public string Id { get; set; }
    public string ProjName { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Username { get; set; }

    // Navigation Properties
    //public User User { get; set; }
    //public Project Project { get; set; }
}

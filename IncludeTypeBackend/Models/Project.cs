namespace IncludeTypeBackend.Models;

public class Project
{
    public string Id { get; set; }
    public string Date { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string About { get; set; }
    public string Documentation { get; set; }

    // Navigation Properties
    //public ICollection<ProjectMember> ProjectMembers { get; set; }
    //public ICollection<ProjectTask> ProjectTasks { get; set; }
    //public ICollection<ProjectIssue> ProjectIssues { get; set; }
}

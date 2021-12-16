namespace IncludeTypeBackend.Models;

public class ProjectTask
{
    public string Id { get; set; }
    public string ProjId { get; set; }
    public string ProjName { get; set; }
    public string Title { get; set; }
    public string Date { get; set; }
    public string Details { get; set; }
    public string Deadline { get; set; }
    public string Assigned { get; set; }
    public bool Completed { get; set; }
    public string Priority { get; set; }
    public string Author { get; set; }
}

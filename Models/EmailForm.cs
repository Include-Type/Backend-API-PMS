namespace IncludeTypeBackend.Models;

public class EmailForm
{
    public string ToEmailAddress { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<FormFile> Attachments { get; set; }
}

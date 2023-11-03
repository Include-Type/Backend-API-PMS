namespace IncludeTypeBackend.Models;

public class Privacy
{
    public string UserId { get; set; }
    public string Name { get; set; } = "Public";
    public string Bio { get; set; } = "Public";
    public string Picture { get; set; } = "Public";
    public string Email { get; set; } = "Public";
    public string Contact { get; set; } = "Public";
    public string Address { get; set; } = "Public";
    public string Education { get; set; } = "Public";
    public string Companies { get; set; } = "Public";
    public string Skills { get; set; } = "Public";
    public string Experience { get; set; } = "Public";
    public string Projects { get; set; } = "Public";
    
    // Navigation Property
    //public User User { get; set; }
}

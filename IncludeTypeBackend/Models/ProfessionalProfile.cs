namespace IncludeTypeBackend.Models;

public class ProfessionalProfile
{
    public string UserId { get; set; }
    public string Education { get; set; } = "";
    public string Companies { get; set; } = "";
    public string Skills { get; set; } = "";
    public int ExperienceYears { get; set; } = 0;
    public int ExperienceMonths { get; set; } = 0;
    public string Projects { get; set; } = "";

    // Navigation Property
    //public User User { get; set; }
}

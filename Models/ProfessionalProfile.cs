namespace IncludeTypeBackend.Models
{
    public class ProfessionalProfile
    {
        public string UserId { get; set; }
        public string Education { get; set; } = null;
        public string Companies { get; set; } = null;
        public string Skills { get; set; } = null;
        public int ExperienceYears { get; set; } = 0;
        public int ExperienceMonths { get; set; } = 0;
        public string Projects { get; set; } = null;
    }
}

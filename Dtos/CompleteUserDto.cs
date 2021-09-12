using IncludeTypeBackend.Models;

namespace IncludeTypeBackend.Dtos
{
    public class CompleteUserDto
    {
        public User User { get; set; }
        public ProfessionalProfile ProfessionalProfile { get; set; }
        public Privacy Privacy { get; set; }
    }
}

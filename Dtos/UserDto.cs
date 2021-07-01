using System.ComponentModel.DataAnnotations;

namespace IncludeTypeBackend.Dtos
{
    public class UserDto
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

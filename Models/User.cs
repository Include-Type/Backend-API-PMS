using System.ComponentModel.DataAnnotations;

namespace IncludeTypeBackend.Models
{
    public class User
    {
        [Key]
        [MaxLength(100)]
        public string UserId { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string Username { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
    }
}

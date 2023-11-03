namespace IncludeTypeBackend.Models;

public class User
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Bio { get; set; } = "";
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Address { get; set; } = "";
    public string Country { get; set; } = "";
    public string City { get; set; } = "";
    public string State { get; set; } = "";
    public string Pincode { get; set; } = "";
    public string Contact { get; set; } = "";
    public string Picture { get; set; } = "";
    public bool IsAdmin { get; set; } = false;

    // Navigation Properties
    //public UserVerification UserVerification { get; set; }
    //public Privacy Privacy { get; set; }
    //public ProfessionalProfile ProfessionalProfile { get; set; }
    //public ProjectMember ProjectMember { get; set; }
}

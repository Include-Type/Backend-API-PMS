namespace IncludeTypeBackend.Dtos;

public class UserVerificationDto
{
    public string UserId { get; set; }
    public string UniqueString { get; set; }
    public string NewPassword { get; set; }
}

namespace IncludeTypeBackend.Models;

public class UserVerification
{
    public string UserId { get; set; }
    public string UniqueString { get; set; }
    public string CreationTime { get; set; }
    public string ExpirationTime { get; set; }
}

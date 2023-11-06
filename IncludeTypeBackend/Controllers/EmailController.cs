namespace IncludeTypeBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly EmailService _email;

    public EmailController(EmailService email) => _email = email;

    [HttpPost("[action]/{email}")]
    public async Task<ActionResult> SendTestEmail(string email)
    {
        try
        {
            EmailForm emailForm = new()
            {
                ToEmailAddress = email,
                Subject = @"Test | #include <TYPE>",
                Body = @"<h3>This is a test mail sent by an API of #include &lt;TYPE&gt;</h3>
                         <h2>🚀</h2>
                         <a href=https://include-type.github.io>HOME PAGE</a>",
                Attachments = null
            };

            await _email.SendEmailAsync(emailForm);
            return Ok("Test email sent successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

namespace IncludeTypeBackend.Services;

public class EmailService
{
    private readonly PostgreSqlContext _db;
    private readonly EmailConfiguration _emailConfig;

    public EmailService(PostgreSqlContext db, IOptions<EmailConfiguration> emailConfig)
    {
        _db = db;
        _emailConfig = emailConfig.Value;
    }

    public async Task SendEmailAsync(EmailForm emailForm)
    {
        BodyBuilder bodyBuilder = new()
        {
            HtmlBody = emailForm.Body
        };

        MimeMessage email = new()
        {
            Sender = MailboxAddress.Parse(_emailConfig.EmailAddress),
            Subject = emailForm.Subject,
            Body = bodyBuilder.ToMessageBody()
        };
        email.To.Add(MailboxAddress.Parse(emailForm.ToEmailAddress));

        using SmtpClient smtp = new();
        await smtp.ConnectAsync(_emailConfig.Host, _emailConfig.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_emailConfig.EmailAddress, _emailConfig.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}

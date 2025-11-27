namespace RDS.Core.Libraries.Services;

public interface IEmailSendService
{
    Task SendMailResendAsync(string toName,
        string toEmail,
        string subject,
        string body);

    Task<bool> SendAsync(string toName,
        string toEmail,
        string subject,
        string body,
        string fromName = "Equipe RDS Devs.",
        string fromEmail = "roberto.rn@gmail.com");
}
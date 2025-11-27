using System.Net;
using System.Net.Mail;
using RDS.Core.Libraries.Services;
using Resend;

namespace RDS.Server.Libraries;

public class EmailSendService(ILogger<EmailSendService> logger, IResend resend): IEmailSendService
{
    public async Task SendMailResendAsync(string toName,
        string toEmail,
        string subject,
        string body)
    {
        var message = new EmailMessage
        {
            From = ConfigurationServer.SmtpResend.FromEmail,
            Subject = subject,
            HtmlBody = body
        };
        message.To.Add(toEmail);

        try
        {
            await resend.EmailSendAsync(message);
            logger.LogInformation("E-mail enviado com sucesso para {toEmail}", toEmail);
        }
        catch (HttpRequestException ex)
        {
            logger.LogWarning(ex, "Falha no Resend API, tentando SMTP para {toEmail}", toEmail);
            await SendAsync(toName, toEmail, subject, body, ConfigurationServer.SmtpResend.UserName, ConfigurationServer.SmtpResend.FromEmail);
        }
        catch (SmtpException ex)
        {
            logger.LogError(ex, "Erro ao enviar e-mail para {toEmail}: {ErrorMessage}", toEmail, ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro inesperado ao enviar e-mail para {toEmail}: {ErrorMessage}", toEmail,
                ex.Message);
        }
    }

    public async Task<bool> SendAsync(string toName,
        string toEmail,
        string subject,
        string body,
        string fromName,
        string fromEmail)
    {
        var smtpClient = new SmtpClient(ConfigurationServer.Smtp.Host, ConfigurationServer.Smtp.Port);

        smtpClient.Credentials = new NetworkCredential(ConfigurationServer.Smtp.UserName, ConfigurationServer.Smtp.Password);
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.EnableSsl = true;
        var mail = new MailMessage();

        if (string.IsNullOrEmpty(toName))
        {
            toName = toEmail;
        }

        mail.From = new MailAddress(fromEmail, fromName);
        mail.To.Add(new MailAddress(toEmail, toName));
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        try
        {
            smtpClient.Timeout = 10000;
            await smtpClient.SendMailAsync(mail);
            logger.LogInformation("E-mail enviado com sucesso para {toEmail}", toEmail);
            return true;
        }
        catch (SmtpException ex)
        {
            logger.LogError(ex, "Erro ao enviar e-mail para {toEmail}: {ErrorMessage}", toEmail, ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro inesperado ao enviar e-mail para {toEmail}: {ErrorMessage}", toEmail,
                ex.Message);
            return false;
        }
    }
}
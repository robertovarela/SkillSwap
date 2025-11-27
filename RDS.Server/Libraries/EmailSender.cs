using Microsoft.AspNetCore.Identity;
using RDS.Core.Libraries.Services;
using RDS.Core.Models;

namespace RDS.Server.Libraries;

public class EmailSender(IEmailSendService emailService) : IEmailSender<ApplicationUser>
{
    public async Task SendConfirmationLinkAsync(ApplicationUser user, string email,
        string confirmationLink)
    {
        await emailService.SendMailResendAsync(user.UserName!, email, "Confirme seu e-mail",
            "<html lang=\"en\"><head></head><body>Por favor confirme seu cadastro clicando no link: " +
            $"<a href='{confirmationLink}'>Confirme</a>.</body></html>");
    }

    public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        await emailService.SendMailResendAsync(user.UserName!, email, "Redefinir senha",
            "<html lang=\"en\"><head></head><body>Clique no link e redefina sua senha " +
            $"<a href='{resetLink}'>Alterar Senha</a>.</body></html>");
    }

    public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        await emailService.SendMailResendAsync(user.UserName!, email, "Alterar senha",
            "<html lang=\"en\"><head></head><body>Por favor, redefina sua senha " +
            $"usando o seguinte código: <br>{resetCode}</body></html>");
    }
}
using Polly;
using RDS.Core.Libraries.Services;

namespace RDS.Server.Libraries;

/// <summary>
/// Serviço de Background para envio de e-mails.
/// </summary>
public class EmailBackgroundSender(IServiceProvider serviceProvider, ILogger<EmailBackgroundSender> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (exception, timespan, retryCount, context) =>
                {
                    logger.LogWarning(exception,
                        "Tentativa {retryCount} falhou. Tentando novamente em {timespan} segundos...",
                        retryCount, timespan.TotalSeconds);
                });

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            var queueService = scope.ServiceProvider.GetRequiredService<EmailQueueService>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailSendService>();

            if (queueService.TryDequeue(out var email) && email != null)
            {
                await retryPolicy.ExecuteAsync(async () =>
                {
                    await emailService.SendMailResendAsync(
                        email.ToName,
                        email.ToEmail,
                        email.Subject,
                        email.Body
                    );
                });
            }

            await Task.Delay(500, stoppingToken);
        }
    }
}
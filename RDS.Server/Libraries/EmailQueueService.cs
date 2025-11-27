using System.Collections.Concurrent;
using RDS.Core.Requests;

namespace RDS.Server.Libraries;

/// <summary>
/// Gerencia a fila de envio de e-mails.
/// </summary>
public class EmailQueueService
{
    private readonly ConcurrentQueue<EmailRequest> _emails = new();

    public void EnqueueEmail(EmailRequest email)
    {
        _emails.Enqueue(email);
    }

    public bool TryDequeue(out EmailRequest? email)
    {
        return _emails.TryDequeue(out email);
    }
}
namespace RDS.Core.Requests;

/// <summary>
/// Representa um pedido de envio de e-mail.
/// </summary>
public class EmailRequest
{
    public string ToName { get; set; } = string.Empty;
    public string ToEmail { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
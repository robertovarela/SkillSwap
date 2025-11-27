namespace RDS.Core.Dtos.Message;

/// <summary>
/// DTO para criação de uma nova mensagem.
/// </summary>
public class MessageCreateDto
{
    public long Id { get; set; }
    public long SenderId { get; set; }
    public long ReceiverId { get; set; }
    public string Content { get; set; } = null!;
    public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow;
    public bool IsRead { get; set; } = false;
    public long? SkillSwapRequestId { get; set; }
}
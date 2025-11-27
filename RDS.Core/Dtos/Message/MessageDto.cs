using RDS.Core.Dtos.ApplicationUser;

namespace RDS.Core.Dtos.Message;

/// <summary>
/// DTO para exibição das mensagens.
/// </summary>
public record MessageDto
{
    public long Id { get; init; }
    public long SenderId { get; init; }
    public long ReceiverId { get; init; }
    public string Content { get; init; } = null!;
    public DateTimeOffset SentAt { get; init; }
    public bool IsRead { get; set; }
    public ApplicationUserDto? Sender { get; set; }
    public ApplicationUserDto? Receiver { get; set; }
    public string? SenderProfessionalName { get; init; }
    public string? ReceiverProfessionalName { get; init; }
}
using System.ComponentModel.DataAnnotations;

namespace RDS.Core.Models;

/// <summary>
/// Mensagem entre usuários na plataforma.
/// </summary>
public class Message
{
    [Key]
    public long Id { get; set; }

    [Required]
    public long SenderId { get; set; }

    [Required]
    public long ReceiverId { get; set; }

    /// <summary>
    /// Conteúdo da mensagem.
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Data/hora do envio da mensagem.
    /// </summary>
    public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Confirmação de leitura da mensagem
    /// </summary>
    public bool IsRead { get; set; }

    // Relacionamentos
    /// <summary>
    /// Proposta a que se refere a mensagem.
    /// </summary>
    public long? SkillSwapRequestId { get; set; }

    // Navegações
    public ApplicationUser? Sender { get; set; }
    public ApplicationUser? Receiver { get; set; }
    public SkillSwapRequest? SkillSwapRequest { get; set; }
}
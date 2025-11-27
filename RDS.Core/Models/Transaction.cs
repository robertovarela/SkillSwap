using System.ComponentModel.DataAnnotations;
 using System.ComponentModel.DataAnnotations.Schema;

namespace RDS.Core.Models;

public class Transaction
{
    public long Id { get; set; }

    public long SenderProfileId { get; set; }
    public ProfessionalProfile SenderProfile { get; set; } = null!;

    public long ReceiverProfileId { get; set; }
    public ProfessionalProfile ReceiverProfile { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    public DateTimeOffset TransactionDate { get; set; }
    [MaxLength(1000, ErrorMessage = "A descrição não pode ter mais que 1000 caracteres")]
    public string Description { get; set; } = string.Empty;
    public long? SkillSwapRequestId { get; set; } // Optional link to a proposal
    public SkillSwapRequest? SkillSwapRequest { get; set; }
}
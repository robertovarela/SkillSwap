using System.ComponentModel.DataAnnotations;

namespace RDS.Core.Enums;

public enum StatusSwapRequest
{
    /// <summary>
    /// Request is pending review (0)
    /// </summary>
    [Display(Name = "Pendente")] Pending,

    /// <summary>
    /// Request was accepted (1)
    /// </summary>
    [Display(Name = "Aceita")] Accepted,

    /// <summary>
    /// Request was rejected (2)
    /// </summary>
    [Display(Name = "Rejeitada")] Rejected,

    /// <summary>
    /// Request was completed (3)
    /// </summary>
    [Display(Name = "Concluída")] Completed,

    /// <summary>
    /// Request was cancelled (4)
    /// </summary>
    [Display(Name = "Cancelada")] Cancelled,

    /// <summary>
    /// Professional B marked the swap as completed, pending confirmation from A (5)
    /// </summary>
    [Display(Name = "Aguardando Confirmação (A)")] CompletionPendingA,

    /// <summary>
    /// Professional A marked the swap as completed, pending confirmation from B (6)
    /// </summary>
    [Display(Name = "Aguardando Confirmação (B)")] CompletionPendingB,

    /// <summary>
    /// A counter-offer was made (7)
    /// </summary>
    [Display(Name = "Contraproposta")] Countered
}

using System.ComponentModel.DataAnnotations;

namespace RDS.Core.Models;

/// <summary>
/// Solicitação de serviço feita por um cliente para um profissional.
/// </summary>
public class ServiceRequest
{
    public long Id { get; set; }

    public long ClientId { get; set; }
    public long ServiceId { get; set; }
    
    /// <summary>
    /// Título da solicitação.
    /// </summary>
    [Required(ErrorMessage = "O título da solicitação é obrigatório")]
    [MaxLength(50, ErrorMessage = "O título da solicitação não pode ter mais que 50 caracteres")]
    public string ServiceTitle { get; set; } = string.Empty;

    /// <summary>
    /// Data em que a solicitação foi feita.
    /// </summary>
    public DateTimeOffset RequestDate { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Observações adicionais do cliente para o profissional.
    /// </summary>
    [MaxLength(500, ErrorMessage = "As observações não podem ter mais que 500 caracteres")]
    public string? Observations { get; set; }

    /// <summary>
    /// Status da solicitação (ex: Pendente, Aceito, Rejeitado, Concluído).
    /// </summary>
    [Required(ErrorMessage = "O status da solicitação é obrigatório")]
    [MaxLength(20, ErrorMessage = "O status não pode ter mais que 20 caracteres")]
    public string Status { get; set; } = "Pendente";

    // Navegações
    public ApplicationUser? Client { get; set; }
    public ServiceOffered? Service { get; set; }
    public Review? Review { get; set; }
}
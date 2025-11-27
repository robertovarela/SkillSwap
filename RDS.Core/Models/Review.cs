using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDS.Core.Models;

/// <summary>
/// Avaliação feita por um cliente para um serviço prestado.
/// </summary>
public class Review
{
    [Key]
    public long Id { get; set; }

    [Required]
    public long ServiceId { get; set; }

    [Required]
    public long ReviewerId { get; set; } // Quem fez a avaliação (ApplicationUser)

    /// <summary>
    /// Nota atribuída (1 a 5).
    /// </summary>
    [Range(1, 5)]
    [Required]
    public int Rating { get; set; }

    /// <summary>
    /// Comentário adicional.
    /// </summary>
    [MaxLength(1000)]
    public string? Comment { get; set; }

    /// <summary>
    /// Data da avaliação.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Navegação
    [ForeignKey(nameof(ServiceId))]
    public ServiceOffered? Service { get; set; }

    [ForeignKey(nameof(ReviewerId))]
    public ApplicationUser? Reviewer { get; set; }
}
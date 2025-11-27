using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using RDS.Core.Libraries.Validations;

namespace RDS.Core.Models;

/// <summary>
/// Serviço oferecido por um profissional.
/// </summary>
public class ServiceOffered
{
    public long Id { get; set; }

    /// <summary>
    /// Título do serviço (ex: "Criação de Landing Pages").
    /// </summary>
    [Required(ErrorMessage = "O campo 'Título' é obrigatório!")]
    [MinLength(3, ErrorMessage = "O título deve ter pelo menos 3 caracteres.")]
    [MaxLength(50, ErrorMessage = "O título deve ter no máximo 50 caracteres.")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Descrição detalhada do serviço.
    /// </summary>
    [Required(ErrorMessage = "O campo 'Descrição' é obrigatório!")]
    [MinLength(10, ErrorMessage = "A descrição deve ter pelo menos 10 caracteres.")]
    [MaxLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres.")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Preço sugerido pelo serviço (opcional para SkillSwap).
    /// </summary>
    [Precision(18, 2)]
    [Range(0, 9999999.99, ErrorMessage = "O preço deve estar entre 0 e 9.999.999,99.")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Define se o profissional está ativo na plataforma.
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Data de criação do serviço.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Navegações
    public long ProfessionalProfileId { get; set; }
    public ProfessionalProfile? ProfessionalProfile { get; set; }

    [Required(ErrorMessage = "Necessário informar a categoria")]
    [MinValue(1, ErrorMessage = "Necessário informar a categoria")]
    public long CategoryId { get; set; }
    public Category? Category { get; set; }

    /// <summary>
    /// Avaliações relacionadas a este serviço.
    /// </summary>
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
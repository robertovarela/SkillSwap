using System.ComponentModel.DataAnnotations;

namespace RDS.Core.Models;

/// <summary>
/// Categoria para classificação dos serviços.
/// </summary>
public class Category
{
    public long Id { get; set; }

    /// <summary>
    /// Nome da categoria.
    /// </summary>
    [Required(ErrorMessage = "O campo 'Nome' é obrigatório!")]
    [StringLength(50)]
    [Display(Name = "Nome")]
    [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres.")]
    [MaxLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres.")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Descrição adicional da categoria.
    /// </summary>
    [Required(ErrorMessage = "O campo 'Descrição' é obrigatório!")]
    [StringLength(200)]
    [Display(Name = "Descrição")]
    [MinLength(3, ErrorMessage = "A descrição deve ter pelo menos 3 caracteres.")]
    [MaxLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
    public string Description { get; set; } = null!;

    public ICollection<ServiceOffered> Services { get; set; } = new List<ServiceOffered>();
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDS.Core.Models;

/// <summary>
/// Representa o perfil profissional de um usuário da plataforma.
/// </summary>
public class ProfessionalProfile
{
    [Key] public long Id { get; set; }

    /// <summary>
    /// ID do usuário (ApplicationUser).
    /// </summary>
    [Required]
    public long UserId { get; set; }

    [ForeignKey(nameof(UserId))] public ApplicationUser? User { get; set; }

    /// <summary>
    /// Nome do perfil profissional.
    /// </summary>
    [Required]
    [Display(Name = "Nome")]
    [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres.")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string ProfessionalName { get; set; } = null!;

    /// <summary>
    /// Biografia ou descrição do profissional.
    /// </summary>
    [Required]
    [Display(Name = "Biografia")]
    [MinLength(10, ErrorMessage = "A biografia deve ter pelo menos 10 caracteres.")]
    [MaxLength(1000, ErrorMessage = "A biografia deve ter no máximo 1000 caracteres.")]
    public string Bio { get; set; } = null!;

    /// <summary>
    /// Especialização principal do profissional.
    /// </summary>
    [Required]
    [MinLength(10, ErrorMessage = "A especialização deve ter pelo menos 10 caracteres.")]
    [Display(Name = "Especialização")]
    [MaxLength(200, ErrorMessage = "A especialização deve ter no máximo 200 caracteres.")]
    public string Expertise { get; set; } = null!;
    
    /// <summary>
    /// Registro Acadêmico (RA) do profissional.
    /// </summary>
    [Required]
    [MaxLength(15)]
    public string AcademicRegistry { get; set; } = null!;

    /// <summary>
    /// CPF do profissional.
    /// </summary>
    [Required]
    [Column(TypeName = "varchar(11)")]
    public Cpf? Cpf { get; set; }

    /// <summary>
    /// Data de nascimento do profissional.
    /// </summary>
    [Required]
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Instituição de ensino do profissional.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string TeachingInstitution { get; set; } = null!;

    /// <summary>
    /// Indica se o profissional possui conta premium.
    /// </summary>
    public bool IsPremium { get; set; }

    /// <summary>
    /// Lista de serviços oferecidos pelo profissional.
    /// </summary>
    public ICollection<ServiceOffered> Services { get; set; } = new List<ServiceOffered>();

    /// <summary>
    /// Avaliações recebidas de clientes.
    /// </summary>
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    /// <summary>
    /// Propiedades de saldo em Skill Dólares
    /// </summary>
    public decimal SkillDolarBalance { get; set; }


    /// <summary>
    /// Propiedades de navegación para el historial de transacciones
    /// </summary>
    public ICollection<Transaction> TransactionsSent { get; set; } = new List<Transaction>();
    public ICollection<Transaction> TransactionsReceived { get; set; } = new List<Transaction>();
}

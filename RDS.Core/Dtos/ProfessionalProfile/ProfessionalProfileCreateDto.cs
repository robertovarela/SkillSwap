using System.ComponentModel.DataAnnotations;

namespace RDS.Core.Dtos.ProfessionalProfile;

public class ProfessionalProfileCreateDto
{
    public long UserId { get; set; }

    [Required]
    public string ProfessionalName { get; set; } = null!;
    public string Bio { get; set; } = null!;
    public string Expertise { get; set; } = null!;
    public bool IsPremium { get; set; } = false;
    
    public string AcademicRegistry { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public string TeachingInstitution { get; set; } = null!;
}

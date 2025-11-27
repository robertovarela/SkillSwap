namespace RDS.Core.Dtos.ProfessionalProfile;

public record ProfessionalProfileDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string ProfessionalName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Expertise { get; set; } = string.Empty;
    public bool IsPremium { get; set; }
    public decimal SkillDolarBalance { get; set; }
    
    public string AcademicRegistry { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string TeachingInstitution { get; set; } = string.Empty;
    public bool HasAssociatedServices { get; set; }
}

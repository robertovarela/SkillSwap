namespace RDS.Core.Dtos.ServiceOffered;

/// <summary>
/// DTO para exibição de serviço.
/// </summary>
public record ServiceOfferedDto
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public long CategoryId { get; set; }
    public long ProfessionalProfileId { get; set; }
    public string? CategoryName { get; set; } = string.Empty;
    public string? CategoryDescription { get; set; } = string.Empty;
    public string? ProfessionalName { get; set; } = string.Empty;
    public bool HasProposals { get; set; }
}
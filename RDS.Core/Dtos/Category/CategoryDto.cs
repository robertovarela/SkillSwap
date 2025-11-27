namespace RDS.Core.Dtos.Category;

/// <summary>
/// DTO para exibição de categoria.
/// </summary>
public record CategoryDto
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool HasAssociatedServices { get; set; }
}

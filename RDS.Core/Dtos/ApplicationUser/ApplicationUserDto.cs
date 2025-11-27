namespace RDS.Core.Dtos.ApplicationUser;

public record ApplicationUserDto
{
    public long Id { get; init; }
    public string UserName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public bool IsProfessional { get; init; }
    public bool IsClient { get; init; }
}
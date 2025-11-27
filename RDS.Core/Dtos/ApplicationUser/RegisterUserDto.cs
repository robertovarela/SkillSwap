namespace RDS.Core.Dtos.ApplicationUser;

public class RegisterUserDto
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public bool IsProfessional { get; set; }
    public bool IsClient { get; set; }
}
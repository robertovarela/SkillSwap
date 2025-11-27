using Microsoft.AspNetCore.Identity;

namespace RDS.Core.Models;

/// <summary>
/// Representa o usuário do sistema (cliente e/ou profissional).
/// </summary>
public class ApplicationUser : IdentityUser<long>
{
    //public long Id { get; set; } // Herda de IdentityUser em Server, aqui é puro
    //public string UserName { get; set; } = string.Empty;
    //public string Email { get; set; } = string.Empty;
    //public string? Phone { get; set; }

    /// <summary>
    /// Indica se o usuário atua como profissional.
    /// </summary>
    public bool IsProfessional { get; set; }

    /// <summary>
    /// Indica se o usuário atua como cliente.
    /// </summary>
    public bool IsClient { get; set; }

    // Navegações
    public ProfessionalProfile? ProfessionalProfile { get; set; }
    public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
    // ReSharper disable once CollectionNeverUpdated.Global
    public ICollection<Message> MessagesSent { get; set; } = new List<Message>();
    // ReSharper disable once CollectionNeverUpdated.Global
    public ICollection<Message> MessagesReceived { get; set; } = new List<Message>();
}
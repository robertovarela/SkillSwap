namespace RDS.Core.Dtos.ServiceOffered;

/// <summary>
/// DTO para criação de um serviço oferecido.
/// </summary>
public class ServiceOfferedCreateDto : ServiceOfferedBaseDto
{
    public long ProfessionalProfileId { get; set; }
}
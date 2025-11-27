namespace RDS.Core.Dtos.ServiceOffered;

/// <summary>
/// DTO para atualização de serviço.
/// </summary>
public class ServiceOfferedUpdateDto : ServiceOfferedBaseDto
{
    public long Id { get; set; }
    public bool HasProposals { get; set; }
}

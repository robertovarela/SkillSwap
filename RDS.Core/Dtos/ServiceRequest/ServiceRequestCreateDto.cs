namespace RDS.Core.Dtos.ServiceRequest;

/// <summary>
/// DTO para criação de uma solicitação de serviço.
/// </summary>
public class ServiceRequestCreateDto
{
    public long ClientId { get; set; }
    public long ServiceId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string ServiceTitle { get; set; } = string.Empty;
    public DateTimeOffset RequestDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Observations { get; set; }
}
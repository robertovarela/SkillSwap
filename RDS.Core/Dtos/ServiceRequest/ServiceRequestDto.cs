namespace RDS.Core.Dtos.ServiceRequest;

/// <summary>
/// DTO para exibição de uma solicitação de serviço.
/// </summary>
public record ServiceRequestDto
{
    public long Id { get; set; }
    public long ClientId { get; set; }
    public long ServiceId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string ServiceTitle { get; set; } = string.Empty;
    public DateTimeOffset RequestDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Observations { get; set; }
}
namespace RDS.Core.Requests;

public class ServiceRequestFilterParams : FilterParams
{
    /// <summary>
    /// Identificador do cliente associado ao pedido de serviço.
    /// Valores possíveis: um número inteiro positivo ou null (não filtrado).
    /// </summary>
    public long? ClientId { get; set; }
}

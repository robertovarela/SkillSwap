namespace RDS.Core.Dtos.SkillSwapRequest;

/// <summary>
/// DTO para registrar uma contraproposta.
/// </summary>
public class CounterOfferDto
{
    public long SkillSwapRequestId { get; set; }
    
    // Contexto do remetente, passado pela camada de apresentação
    public long SenderUserId { get; set; }
    public long SenderProfessionalId { get; set; }
    
    /// <summary>
    /// Opcional: O novo serviço oferecido pelo Profissional A na contraproposta.
    /// </summary>
    public long? NewServiceAId { get; set; }
    
    /// <summary>
    /// Opcional: O novo valor em dinheiro oferecido pelo Profissional A na contraproposta.
    /// </summary>
    public decimal? NewOfferedAmount { get; set; }
    
    /// <summary>
    /// A mensagem que acompanha a contraproposta.
    /// </summary>
    public string Message { get; set; } = string.Empty;
}

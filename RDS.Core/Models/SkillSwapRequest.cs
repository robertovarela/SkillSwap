﻿using System.ComponentModel.DataAnnotations;
 using System.ComponentModel.DataAnnotations.Schema;
 using RDS.Core.Enums;

namespace RDS.Core.Models;

/// <summary>
/// Solicitação de troca de habilidades entre dois profissionais.
/// </summary>
public class SkillSwapRequest
{
    public long Id { get; init; }

    /// <sumary>
    /// Profissional que fez a solicitação de troca
    /// </sumary>
    public long ProfessionalAId { get; init; }

    /// <sumary>
    /// Profissional que recebeu a solicitação de troca
    /// </sumary>
    public long ProfessionalBId { get; init; }

    /// <summary>
    /// Serviço oferecido pelo profissional A.
    /// </summary>
    public long? ServiceAId { get; set; }

    /// <summary>
    /// Serviço oferecido pelo profissional B.
    /// </summary>
    public long ServiceBId { get; init; }

    /// <summary>
    /// Data da proposta de troca.
    /// </summary>
    public DateTimeOffset SwapDate { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Situação da troca (ex: Pendente, Aceita, Rejeitada).
    /// </summary>
    [Display(Name = "Situação")]
    public StatusSwapRequest Status { get; set; } = StatusSwapRequest.Pending;

    /// <summary>
    /// Valor em SkillDólares oferecido em vez de um serviço de troca.
    /// </summary>
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? OfferedAmount { get; set; }


    // Navegações
    public ProfessionalProfile? ProfessionalA { get; init; }
    public ProfessionalProfile? ProfessionalB { get; init; }
    public ServiceOffered? ServiceA { get; init; }
    public ServiceOffered? ServiceB { get; init; }
    public ICollection<Message> Messages { get; set; } = new List<Message>();}
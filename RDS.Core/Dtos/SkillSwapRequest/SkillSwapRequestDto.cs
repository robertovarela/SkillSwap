﻿using RDS.Core.Enums;

namespace RDS.Core.Dtos.SkillSwapRequest;

public record SkillSwapRequestDto
{
    public long Id { get; init; }
    public long ProfessionalAId { get; init; }
    public long ProfessionalBId { get; init; }
    public long? ServiceAId { get; init; }
    public long ServiceBId { get; init; }
    public DateTimeOffset SwapDate { get; init; }
    public StatusSwapRequest Status { get; init; }

    public string? ProfessionalAName { get; init; }
    public string? ProfessionalBName { get; init; }
    public string? ServiceADescription { get; init; }
    public string? ServiceBDescription { get; init; }
    public string? ServiceATitle { get; init; }
    public string? ServiceBTitle { get; init; }
    public decimal? OfferedAmount { get; init; }
}
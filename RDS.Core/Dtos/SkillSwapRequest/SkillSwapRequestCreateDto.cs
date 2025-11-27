﻿using RDS.Core.Enums;

namespace RDS.Core.Dtos.SkillSwapRequest;

public class SkillSwapRequestCreateDto
{
    public long ProfessionalBId { get; set; }
    public long ProfessionalAId { get; set; }
    public long ServiceBId { get; set; }
    public long? ServiceAId { get; set; }
    public DateTimeOffset SwapDate { get; set; }
    public StatusSwapRequest Status { get; set; } = StatusSwapRequest.Pending;
    public decimal? OfferedAmount { get; set; }
}
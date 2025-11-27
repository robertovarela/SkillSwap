using RDS.Core.Enums;

namespace RDS.Core.Dtos.SkillSwapRequest;

public class SkillSwapUpdateStatusDto
{
    public long Id { get; set; }
    public StatusSwapRequest Status { get; set; }
}
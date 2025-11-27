using RDS.Core.Libraries.Services;

namespace RDS.Infrastructure.Services;

public class SkillSwapNotificationService : ISkillSwapNotificationService
{
    public event Action<long>? OnSkillSwapUpdated;

    public Task NotifySkillSwapUpdated(long skillSwapId)
    {
        OnSkillSwapUpdated?.Invoke(skillSwapId);
        return Task.CompletedTask;
    }

    public event Action<long>? OnMessageSent;

    public Task NotifyMessageSent(long skillSwapId)
    {
        OnMessageSent?.Invoke(skillSwapId);
        return Task.CompletedTask;
    }
}

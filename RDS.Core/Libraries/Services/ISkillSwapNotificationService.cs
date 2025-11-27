namespace RDS.Core.Libraries.Services;

public interface ISkillSwapNotificationService
{
    event Action<long>? OnSkillSwapUpdated;
    Task NotifySkillSwapUpdated(long skillSwapId);

    event Action<long>? OnMessageSent;
    Task NotifyMessageSent(long skillSwapId);
}

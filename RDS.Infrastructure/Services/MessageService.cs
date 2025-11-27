using RDS.Core.Dtos.Message;
using RDS.Core.Interfaces;
using RDS.Core.Libraries.Services;
using RDS.Core.Models;
using RDS.Core.Requests;
using RDS.Core.Utils;

namespace RDS.Infrastructure.Services;

public class MessageService(IMessageRepository repo, ISkillSwapNotificationService notificationService) : IMessageService
{
    public async Task<IEnumerable<MessageDto>> GetConversationAsync(long senderId, long receiverId)
    {
        return await repo.GetConversationAsync(senderId, receiverId);
    }

    public async Task<MessageDto?> GetByIdAsync(long id, long userId)
    {
        return await repo.GetByIdAsync(id, userId);
    }

    public async Task<long> AddAsync(MessageCreateDto message)
    {
        var messageId = await repo.AddAsync(message);

        // Notifica que uma nova mensagem foi enviada (se estiver associada a uma proposta)
        if (message.SkillSwapRequestId.HasValue)
        {
            await notificationService.NotifyMessageSent(message.SkillSwapRequestId.Value);
        }

        return messageId;
    }

    public Task UpdateAsync(MessageUpdateDto message)
    {
        return repo.UpdateAsync(message);
    }

    public async Task DeleteAsync(long id)
    {
        await repo.DeleteAsync(id);
    }

    public Task<PaginatedResult<MessageDto>> GetPagedAsync(FilterParams filter, long userId, long? receiverId)
    {
        return repo.GetPagedAsync(filter, userId, receiverId);
    }

// Adicione este método
    public async Task<IEnumerable<MessageDto>> GetMessagesByProposalIdAsync(long proposalId, long userId)
    {
        return await repo.GetMessagesByProposalIdAsync(proposalId, userId);
    }
}
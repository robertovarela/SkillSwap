using RDS.Core.Dtos.Message;
using RDS.Core.Requests;
using RDS.Core.Utils;

namespace RDS.Core.Libraries.Services;

public interface IMessageService
{
    Task<IEnumerable<MessageDto>> GetConversationAsync(long senderId, long receiverId);
    Task<MessageDto?> GetByIdAsync(long id, long userId);
    Task<long> AddAsync(MessageCreateDto message);
    Task UpdateAsync(MessageUpdateDto message);
    Task DeleteAsync(long id);
    Task<PaginatedResult<MessageDto>> GetPagedAsync(FilterParams filter, long userId, long? receiverId);
    Task<IEnumerable<MessageDto>> GetMessagesByProposalIdAsync(long proposalId, long userId);
}
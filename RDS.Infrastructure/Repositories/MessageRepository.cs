using Microsoft.EntityFrameworkCore;
using RDS.Core.Dtos.ApplicationUser;
using RDS.Core.Dtos.Message;
using RDS.Core.Interfaces;
using RDS.Core.Models;
using RDS.Core.Requests;
using RDS.Core.Utils;
using RDS.Persistence;

namespace RDS.Infrastructure.Repositories;

public class MessageRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : IMessageRepository
{
    public async Task<IEnumerable<MessageDto>> GetConversationAsync(long senderId, long receiverId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var messages = await (
            from m in context.Messages.AsNoTracking()
            join senderProfile in context.ProfessionalProfiles on m.SenderId equals senderProfile.UserId into
                senderProfiles
            from senderProfile in senderProfiles.DefaultIfEmpty()
            join receiverProfile in context.ProfessionalProfiles on m.ReceiverId equals receiverProfile.UserId into
                receiverProfiles
            from receiverProfile in receiverProfiles.DefaultIfEmpty()
            join senderUser in context.Users on m.SenderId equals senderUser.Id
            join receiverUser in context.Users on m.ReceiverId equals receiverUser.Id
            where
                (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                (m.SenderId == receiverId && m.ReceiverId == senderId)
            orderby m.SentAt descending
            select new MessageDto
            {
                Id = m.Id,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Content = m.Content,
                SentAt = m.SentAt,
                IsRead = m.IsRead,
                Sender = new ApplicationUserDto
                {
                    Id = senderUser.Id,
                    UserName = senderUser.UserName!,
                    Email = senderUser.Email!,
                    IsProfessional = senderUser.IsProfessional,
                    IsClient = senderUser.IsClient
                },
                Receiver = new ApplicationUserDto
                {
                    Id = receiverUser.Id,
                    UserName = receiverUser.UserName!,
                    Email = receiverUser.Email!,
                    IsProfessional = receiverUser.IsProfessional,
                    IsClient = receiverUser.IsClient
                },
                SenderProfessionalName = senderProfile != null ? senderProfile.ProfessionalName : null,
                ReceiverProfessionalName = receiverProfile != null ? receiverProfile.ProfessionalName : null
            }
        ).ToListAsync();

        return messages;
    }

    public async Task<MessageDto?> GetByIdAsync(long id, long userId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var message = await (
            from m in context.Messages.AsNoTracking()
            join senderUser in context.Users on m.SenderId equals senderUser.Id
            join receiverUser in context.Users on m.ReceiverId equals receiverUser.Id
            join senderProfile in context.ProfessionalProfiles on m.SenderId equals senderProfile.UserId into
                senderProfiles
            from senderProfile in senderProfiles.DefaultIfEmpty()
            join receiverProfile in context.ProfessionalProfiles on m.ReceiverId equals receiverProfile.UserId into
                receiverProfiles
            from receiverProfile in receiverProfiles.DefaultIfEmpty()
            where m.Id == id
            select new MessageDto
            {
                Id = m.Id,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Content = m.Content,
                SentAt = m.SentAt,
                IsRead = m.IsRead,
                Sender = new ApplicationUserDto
                {
                    Id = senderUser.Id,
                    UserName = senderUser.UserName!,
                    Email = senderUser.Email!,
                    IsProfessional = senderUser.IsProfessional,
                    IsClient = senderUser.IsClient
                },
                Receiver = new ApplicationUserDto
                {
                    Id = receiverUser.Id,
                    UserName = receiverUser.UserName!,
                    Email = receiverUser.Email!,
                    IsProfessional = receiverUser.IsProfessional,
                    IsClient = receiverUser.IsClient
                },
                SenderProfessionalName = senderProfile != null ? senderProfile.ProfessionalName : null,
                ReceiverProfessionalName = receiverProfile != null ? receiverProfile.ProfessionalName : null
            }
        ).FirstOrDefaultAsync();

        return message;
    }

    public async Task<long> AddAsync(MessageCreateDto message)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var entity = new Message
        {
            SenderId = message.SenderId,
            ReceiverId = message.ReceiverId,
            Content = message.Content,
            SentAt = DateTimeOffset.UtcNow,
            IsRead = false,
            SkillSwapRequestId = message.SkillSwapRequestId
        };
        context.Messages.Add(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(MessageUpdateDto message)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var existing = await context.Messages.FirstOrDefaultAsync(m => m.Id == message.Id);

        if (existing == null)
        {
            throw new KeyNotFoundException($"Message with ID {message.Id} not found.");
        }

        existing.IsRead = message.IsRead;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var message = await context.Messages.FindAsync(id);
        if (message != null)
        {
            context.Messages.Remove(message);
            await context.SaveChangesAsync();
        }
    }

    public async Task<PaginatedResult<MessageDto>> GetPagedAsync(FilterParams filter, long userId, long? receiverId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var query =
            from m in context.Messages.AsNoTracking()
            join senderProfile in context.ProfessionalProfiles on m.SenderId equals senderProfile.UserId into
                senderProfiles
            from senderProfile in senderProfiles.DefaultIfEmpty()
            join receiverProfile in context.ProfessionalProfiles on m.ReceiverId equals receiverProfile.UserId into
                receiverProfiles
            from receiverProfile in receiverProfiles.DefaultIfEmpty()
            join senderUser in context.Users on m.SenderId equals senderUser.Id
            join receiverUser in context.Users on m.ReceiverId equals receiverUser.Id
            where
                (receiverId == null ||
                 (m.SenderId == userId && m.ReceiverId == receiverId) ||
                 (m.SenderId == receiverId && m.ReceiverId == userId))
            select new MessageDto
            {
                Id = m.Id,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Content = m.Content,
                SentAt = m.SentAt,
                IsRead = m.IsRead,
                Sender = new ApplicationUserDto
                {
                    Id = senderUser.Id,
                    UserName = senderUser.UserName!,
                    Email = senderUser.Email!,
                    IsProfessional = senderUser.IsProfessional,
                    IsClient = senderUser.IsClient
                },
                Receiver = new ApplicationUserDto
                {
                    Id = receiverUser.Id,
                    UserName = receiverUser.UserName!,
                    Email = receiverUser.Email!,
                    IsProfessional = receiverUser.IsProfessional,
                    IsClient = receiverUser.IsClient
                },
                SenderProfessionalName = senderProfile != null ? senderProfile.ProfessionalName : null,
                ReceiverProfessionalName = receiverProfile != null ? receiverProfile.ProfessionalName : null
            };

        if (!string.IsNullOrEmpty(filter.Search))
            query = query.Where(m => m.Content.Contains(filter.Search));

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderByDescending(m => m.SentAt)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new PaginatedResult<MessageDto>
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<IEnumerable<MessageDto>> GetMessagesByProposalIdAsync(long proposalId, long userId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.Messages
            .AsNoTracking()
            .Where(m => m.SkillSwapRequestId == proposalId)
            .OrderBy(m => m.SentAt)
            .Select(m => new MessageDto
            {
                Id = m.Id,
                Content = m.Content,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                SentAt = m.SentAt,
                IsRead = m.IsRead
            })
            .ToListAsync();
    }
}
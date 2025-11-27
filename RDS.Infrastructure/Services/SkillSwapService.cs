using RDS.Core.Dtos.Message;
using RDS.Core.Dtos.SkillSwapRequest;
using RDS.Core.Enums;
using RDS.Core.Exceptions;
using RDS.Core.Interfaces;
using RDS.Core.Libraries.Services;

namespace RDS.Infrastructure.Services;

public class SkillSwapService(
    ISkillSwapRepository repo,
    ISkillSwapNotificationService notificationService,
    IMessageService messageService,
    IProfessionalProfileRepository professionalProfileRepository) : ISkillSwapService
{
    public async Task<SkillSwapRequestDto?> GetByIdAsync(long id, long professionalId)
        => await repo.GetByIdAsync(id, professionalId);

    public Task<SkillSwapUpdateStatusDto?> GetForEditAsync(long id, long professionalId)
    {
        return repo.GetForEditAsync(id, professionalId);
    }

    public async Task<IEnumerable<SkillSwapRequestDto>> GetByProfessionalIdAsync(long professionalId)
        => await repo.GetByProfessionalIdAsync(professionalId);

    public Task<IEnumerable<SkillSwapRequestDto>> GetAllAsync()
        => repo.GetAllAsync();

    public Task<IEnumerable<SkillSwapRequestDto>> GetAllAdminAsync()
        => repo.GetAllAdminAsync();

    public async Task<long> AddAsync(SkillSwapRequestCreateDto dto)
    {
        var newId = await repo.AddAsync(dto);
        await notificationService.NotifySkillSwapUpdated(newId);
        return newId;
    }

    public async Task UpdateStatusAsync(long id, StatusSwapRequest status)
    {
        await repo.UpdateStatusAsync(id, status);
        await notificationService.NotifySkillSwapUpdated(id);
    }

    public async Task MakeCounterOfferAsync(CounterOfferDto dto)
    {
        await repo.MakeCounterOfferAsync(dto);

        var swap = await repo.GetByIdAsync(dto.SkillSwapRequestId, dto.SenderProfessionalId)
            ?? throw new KeyNotFoundException("Proposta de troca não encontrada após a contraproposta.");

        var receiverProfessionalId = dto.SenderProfessionalId == swap.ProfessionalAId 
            ? swap.ProfessionalBId 
            : swap.ProfessionalAId;
        
        var receiverProfile = await professionalProfileRepository.GetByIdAsync(receiverProfessionalId)
            ?? throw new KeyNotFoundException("Perfil do destinatário não encontrado.");

        var messageDto = new MessageCreateDto
        {
            SenderId = dto.SenderUserId,
            ReceiverId = receiverProfile.UserId,
            Content = dto.Message,
            SentAt = DateTimeOffset.UtcNow,
            IsRead = false,
            SkillSwapRequestId = dto.SkillSwapRequestId
        };
        await messageService.AddAsync(messageDto);

        await notificationService.NotifySkillSwapUpdated(dto.SkillSwapRequestId);
    }

    public async Task AcceptOfferAsync(long skillSwapId, long acceptingProfessionalId)
    {
        var swap = await repo.GetByIdAsync(skillSwapId, acceptingProfessionalId) 
                   ?? throw new KeyNotFoundException("Proposta de troca não encontrada.");

        if (swap.Status == StatusSwapRequest.Countered && swap.OfferedAmount.HasValue)
        {
            var proposerProfile = await professionalProfileRepository.GetByIdAsync(swap.ProfessionalAId) 
                                  ?? throw new KeyNotFoundException("Perfil do proponente não encontrado.");
            
            var receiverProfile = await professionalProfileRepository.GetByIdAsync(swap.ProfessionalBId) 
                                  ?? throw new KeyNotFoundException("Perfil do destinatário não encontrado.");

            if (proposerProfile.Id == acceptingProfessionalId)
            {
                var cost = swap.OfferedAmount.Value;
                if (proposerProfile.SkillDolarBalance < cost)
                {
                    throw new InsufficientBalanceException("Saldo de SkillDólares insuficiente para aceitar esta contraproposta.");
                }

                proposerProfile.SkillDolarBalance -= cost;
                receiverProfile.SkillDolarBalance += cost;

                await professionalProfileRepository.UpdateBalanceAsync(proposerProfile.Id, proposerProfile.SkillDolarBalance);
                await professionalProfileRepository.UpdateBalanceAsync(receiverProfile.Id, receiverProfile.SkillDolarBalance);
            }
        }
        
        await repo.UpdateStatusAsync(skillSwapId, StatusSwapRequest.Accepted);
        await notificationService.NotifySkillSwapUpdated(skillSwapId);
    }
}

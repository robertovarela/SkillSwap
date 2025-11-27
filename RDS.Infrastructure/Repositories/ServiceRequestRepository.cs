using Microsoft.EntityFrameworkCore;
using RDS.Core.Interfaces;
using RDS.Core.Models;
using RDS.Persistence;

namespace RDS.Infrastructure.Repositories;

public class ServiceRequestRepository(ApplicationDbContext context) : IServiceRequestRepository
{
    public async Task<ServiceRequest?> GetByIdAsync(long id)
    {
        return await context.ServiceRequests
            .Include(r => r.Service)
            .ThenInclude(s => s!.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<ServiceRequest>> GetByClientIdAsync(long clientId)
    {
        return await context.ServiceRequests
            .Where(r => r.ClientId == clientId)
            .Include(r => r.Service)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<ServiceRequest>> GetByServiceIdAsync(long serviceId)
    {
        return await context.ServiceRequests
            .Where(r => r.ServiceId == serviceId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<ServiceRequest>> GetAllAsync()
    {
        return await context.ServiceRequests
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(ServiceRequest request)
    {
        context.ServiceRequests.Add(request);
        await context.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(long id, string status)
    {
        var request = await context.ServiceRequests.FindAsync(id);
        if (request != null)
        {
            request.Status = status;
            await context.SaveChangesAsync();
        }
    }
}
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RDS.Core.Models;
using RDS.Persistence.ValueConverters;

namespace RDS.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ProfessionalProfile> ProfessionalProfiles { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ServiceOffered> ServiceOffered { get; set; }
    public DbSet<ServiceRequest> ServiceRequests { get; set; }
    public DbSet<SkillSwapRequest> SkillSwapRequests { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplica todas as configurações de mapeamento do assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        // Configura o conversor para todas as propriedades do tipo Cpf
        configurationBuilder.Properties<Cpf>()
            .HaveConversion<CpfValueConverter>();
    }
}

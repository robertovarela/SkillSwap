using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RDS.Core.Enums;
using RDS.Core.Models;

namespace RDS.Persistence.Data.Mappings;

public class SkillSwapRequestMapping : IEntityTypeConfiguration<SkillSwapRequest>
{
    public void Configure(EntityTypeBuilder<SkillSwapRequest> builder)
    {
        builder.ToTable("SkillSwapRequest");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProfessionalAId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.Property(x => x.ProfessionalBId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.Property(x => x.ServiceAId)
            .IsRequired(false)
            .HasColumnType("BIGINT");

        builder.Property(x => x.ServiceBId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.Property(x => x.SwapDate)
            .IsRequired()
            .HasColumnType("DATETIMEOFFSET");

        builder.Property(x => x.Status)
            .IsRequired()
            .HasColumnType("INT")
            .HasDefaultValue(StatusSwapRequest.Pending);

        builder.Property(x => x.OfferedAmount)
            .IsRequired(false)
            .HasColumnType("DECIMAL(18,2)");

        // Relacionamentos
        builder.HasOne(r => r.ProfessionalA)
            .WithMany()
            .HasForeignKey(r => r.ProfessionalAId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.ProfessionalB)
            .WithMany()
            .HasForeignKey(r => r.ProfessionalBId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.ServiceA)
            .WithMany()
            .HasForeignKey(r => r.ServiceAId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.ServiceB)
            .WithMany()
            .HasForeignKey(r => r.ServiceBId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Messages)
            .WithOne(m => m.SkillSwapRequest)
            .HasForeignKey(m => m.SkillSwapRequestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}


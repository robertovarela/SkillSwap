using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RDS.Core.Models;

namespace RDS.Persistence.Data.Mappings;

public class ServiceRequestMapping : IEntityTypeConfiguration<ServiceRequest>
{
    public void Configure(EntityTypeBuilder<ServiceRequest> builder)
    {
        builder.ToTable("ServiceRequest");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClientId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.Property(x => x.ServiceId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.Property(x => x.ServiceTitle)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

        builder.Property(x => x.RequestDate)
            .IsRequired()
            .HasColumnType("DATETIMEOFFSET");

        builder.Property(x => x.Observations)
            .IsRequired(false)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(500);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20)
            .HasDefaultValue("Pendente");

        // Relacionamentos
        builder.HasOne(r => r.Client)
            .WithMany()
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Service)
            .WithMany()
            .HasForeignKey(r => r.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RDS.Core.Models;

namespace RDS.Persistence.Data.Mappings;

public class ServiceOfferedMapping : IEntityTypeConfiguration<ServiceOffered>
{
    public void Configure(EntityTypeBuilder<ServiceOffered> builder)
    {
        builder.ToTable("ServiceOffered");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(1000);

        builder.Property(x => x.Price)
            .IsRequired(false)
            .HasColumnType("DECIMAL(18,2)");

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasColumnType("BIT")
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("DATETIMEOFFSET");

        builder.Property(x => x.ProfessionalProfileId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.Property(x => x.CategoryId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.HasOne(x => x.ProfessionalProfile)
            .WithMany(p => p.Services)
            .HasForeignKey(x => x.ProfessionalProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}


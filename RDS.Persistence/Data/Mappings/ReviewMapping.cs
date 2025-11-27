using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RDS.Core.Models;

namespace RDS.Persistence.Data.Mappings;

public class ReviewMapping : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Review");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ServiceId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.Property(x => x.ReviewerId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.Property(x => x.Rating)
            .IsRequired()
            .HasColumnType("INT");

        builder.Property(x => x.Comment)
            .IsRequired(false)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("DATETIMEOFFSET");

        // Relacionamentos
        builder.HasOne(x => x.Reviewer)
            .WithMany()
            .HasForeignKey(x => x.ReviewerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Service)
            .WithMany(s => s.Reviews)
            .HasForeignKey(r => r.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}


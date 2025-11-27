using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RDS.Core.Models;

namespace RDS.Persistence.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SenderProfileId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.Property(x => x.ReceiverProfileId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("DECIMAL(18,2)");

        builder.Property(x => x.TransactionDate)
            .IsRequired()
            .HasColumnType("DATETIMEOFFSET");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(1000);

        builder.Property(x => x.SkillSwapRequestId)
            .IsRequired(false)
            .HasColumnType("BIGINT");

        // Relacionamentos
        builder.HasOne(x => x.SkillSwapRequest)
            .WithMany()
            .HasForeignKey(x => x.SkillSwapRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.SenderProfile)
            .WithMany(p => p.TransactionsSent)
            .HasForeignKey(t => t.SenderProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.ReceiverProfile)
            .WithMany(p => p.TransactionsReceived)
            .HasForeignKey(t => t.ReceiverProfileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}


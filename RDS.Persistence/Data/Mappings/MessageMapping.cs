using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RDS.Core.Models;

namespace RDS.Persistence.Data.Mappings;

public class MessageMapping : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Message");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SenderId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.Property(x => x.ReceiverId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.Property(x => x.Content)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(1000);

        builder.Property(x => x.SentAt)
            .IsRequired()
            .HasColumnType("DATETIMEOFFSET");

        builder.Property(x => x.IsRead)
            .IsRequired()
            .HasColumnType("BIT")
            .HasDefaultValue(false);

        builder.Property(x => x.SkillSwapRequestId)
            .IsRequired(false)
            .HasColumnType("BIGINT");

        builder.HasOne(m => m.Sender)
            .WithMany(u => u.MessagesSent)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Receiver)
            .WithMany(u => u.MessagesReceived)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
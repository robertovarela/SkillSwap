using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RDS.Core.Models;

namespace RDS.Persistence.Data.Mappings;

public class ProfessionalProfileMapping : IEntityTypeConfiguration<ProfessionalProfile>
{
    public void Configure(EntityTypeBuilder<ProfessionalProfile> builder)
    {
        builder.ToTable("ProfessionalProfile");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnType("BIGINT");

        builder.Property(x => x.ProfessionalName)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);

        builder.Property(x => x.Bio)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(1000);

        builder.Property(x => x.Expertise)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(200);

        builder.Property(x => x.AcademicRegistry)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(15);

        builder.Property(x => x.Cpf)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(11);

        builder.Property(x => x.BirthDate)
            .IsRequired()
            .HasColumnType("DATE");

        builder.Property(x => x.TeachingInstitution)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);

        builder.Property(x => x.IsPremium)
            .IsRequired()
            .HasColumnType("BIT")
            .HasDefaultValue(false);

        builder.Property(x => x.SkillDolarBalance)
            .IsRequired()
            .HasColumnType("DECIMAL(18,2)")
            .HasDefaultValue(0);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

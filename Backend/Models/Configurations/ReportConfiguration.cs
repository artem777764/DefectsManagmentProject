using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<ReportEntity>
{
    void IEntityTypeConfiguration<ReportEntity>.Configure(EntityTypeBuilder<ReportEntity> builder)
    {
        builder.HasKey(r => new { r.ProjectId, r.FileId });

        builder.HasOne(r => r.Project)
               .WithMany(p => p.Reports)
               .HasForeignKey(r => r.ProjectId);
    }
}
using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations;

public class DefectConfiguration : IEntityTypeConfiguration<DefectEntity>
{
    void IEntityTypeConfiguration<DefectEntity>.Configure(EntityTypeBuilder<DefectEntity> builder)
    {
        builder.HasKey(d => d.Id);

        builder.HasOne(d => d.Project)
               .WithMany(p => p.Defects)
               .HasForeignKey(d => d.ProjectId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasOne(d => d.DefectStatus)
               .WithMany(ds => ds.Defects)
               .HasForeignKey(d => d.StatusId);

        builder.HasOne(d => d.Priority)
               .WithMany(p => p.Defects)
               .HasForeignKey(d => d.PriorityId);

        builder.HasOne(d => d.Creator)
               .WithMany(c => c.DefectsCreatedBy)
               .HasForeignKey(d => d.CreatorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.Executor)
               .WithMany(e => e.DefectsExecutedBy)
               .HasForeignKey(d => d.ExecutorId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
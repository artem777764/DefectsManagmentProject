using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
{
    void IEntityTypeConfiguration<EmployeeEntity>.Configure(EntityTypeBuilder<EmployeeEntity> builder)
    {
        builder.HasKey(e => new { e.ProjectId, e.EmployeeId });

        builder.HasOne(e => e.Project)
               .WithMany(p => p.Employers)
               .HasForeignKey(e => e.ProjectId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        
        builder.HasOne(e => e.Employee)
               .WithMany(em => em.Employers)
               .HasForeignKey(e => e.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
    }
}
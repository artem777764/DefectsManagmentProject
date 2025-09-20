using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations;

public class HistoryConfiguration : IEntityTypeConfiguration<HistoryEntity>
{
    void IEntityTypeConfiguration<HistoryEntity>.Configure(EntityTypeBuilder<HistoryEntity> builder)
    {
        builder.HasKey(h => h.Id);

        builder.HasOne(h => h.Defect)
               .WithMany(d => d.History)
               .HasForeignKey(h => h.DefectId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasOne(h => h.User)
               .WithMany(u => u.History)
               .HasForeignKey(h => h.UserId);

        builder.HasOne(h => h.DefectStatus)
               .WithMany(ds => ds.History)
               .HasForeignKey(h => h.DefectStatusId);
    }
}
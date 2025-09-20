using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations;

public class AttachmentConfiguration : IEntityTypeConfiguration<AttachmentEntity>
{
    void IEntityTypeConfiguration<AttachmentEntity>.Configure(EntityTypeBuilder<AttachmentEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne(a => a.Defect)
               .WithMany(d => d.Attachments)
               .HasForeignKey(a => a.DefectId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasOne(a => a.UploadedBy)
               .WithMany(u => u.Attachments)
               .HasForeignKey(a => a.UploadedById)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
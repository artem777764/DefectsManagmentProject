using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    void IEntityTypeConfiguration<CommentEntity>.Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Defect)
               .WithMany(d => d.Comments)
               .HasForeignKey(c => c.DefectId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasOne(c => c.User)
               .WithMany(u => u.Comments)
               .HasForeignKey(c => c.UserId);
    }
}
using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<ProjectEntity>
{
    void IEntityTypeConfiguration<ProjectEntity>.Configure(EntityTypeBuilder<ProjectEntity> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
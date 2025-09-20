using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations;

public class PriorityConfiguration : IEntityTypeConfiguration<PriorityEntity>
{
    void IEntityTypeConfiguration<PriorityEntity>.Configure(EntityTypeBuilder<PriorityEntity> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
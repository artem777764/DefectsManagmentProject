using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations;

public class DefectStatusConfiguration : IEntityTypeConfiguration<DefectStatusEntity>
{
    void IEntityTypeConfiguration<DefectStatusEntity>.Configure(EntityTypeBuilder<DefectStatusEntity> builder)
    {
        builder.HasKey(ds => ds.Id);
    }
}
using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    void IEntityTypeConfiguration<RoleEntity>.Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(r => r.Id);
    }
}
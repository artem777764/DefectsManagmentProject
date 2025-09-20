using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Models.Configurations;

public class UserDataConfiguration : IEntityTypeConfiguration<UserDataEntity>
{
    void IEntityTypeConfiguration<UserDataEntity>.Configure(EntityTypeBuilder<UserDataEntity> builder)
    {
        builder.HasKey(ud => ud.Id);

        builder.HasOne(ud => ud.User)
               .WithOne(u => u.UserData)
               .HasForeignKey<UserDataEntity>()
               .IsRequired();
    }
}
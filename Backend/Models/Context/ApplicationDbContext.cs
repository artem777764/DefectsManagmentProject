using Backend.Models.Configurations;
using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new DefectConfiguration());
        modelBuilder.ApplyConfiguration(new DefectStatusConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new HistoryConfiguration());
        modelBuilder.ApplyConfiguration(new PriorityConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new ReportConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserDataConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<AttachmentEntity> Attachments { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<DefectEntity> Defects { get; set; }
    public DbSet<DefectStatusEntity> DefectStatuses { get; set; }
    public DbSet<EmployeeEntity> Employers { get; set; }
    public DbSet<HistoryEntity> History { get; set; }
    public DbSet<PriorityEntity> Priorities { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ReportEntity> Reports { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserDataEntity> UserDatas { get; set; }
    public DbSet<UserEntity> Users { get; set; }
}
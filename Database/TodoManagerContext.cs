using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoManager.Models.Database;

namespace TodoManager.Database;

public class TodoManagerContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectType> ProjectTypes { get; set; }

    public TodoManagerContext(DbContextOptions<TodoManagerContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>((EntityTypeBuilder<User> userEntity) =>
        {
            userEntity.ToTable("user");

            userEntity.Property((User user) => user.Id).HasColumnName<int>("id");
            userEntity.Property((User user) => user.Name).HasColumnName<string>("name");
            userEntity.Property((User user) => user.Login).HasColumnName<string>("login");
            userEntity.Property((User user) => user.Password).HasColumnName<string>("password");
            userEntity.Property((User user) => user.CreatedAt).HasColumnName<DateTime>("created_at");
            userEntity.Property((User user) => user.UpdatedAt).HasColumnName<DateTime?>("updated_at");

            userEntity.HasKey((User user) => user.Id);
            userEntity.HasIndex((User user) => user.Login).IsUnique();
            userEntity.Property<DateTime>((User user) => user.CreatedAt)
                .HasDefaultValueSql<DateTime>("getdate()");
        });

        modelBuilder.Entity<Project>((EntityTypeBuilder<Project> projectEntity) =>
        {
            projectEntity.ToTable("project");

            projectEntity.Property((Project project) => project.Id).HasColumnName<int>("id");
            projectEntity.Property((Project project) => project.ProjectTypeId).HasColumnName("project_type_id");

            projectEntity.HasKey((Project project) => project.Id);
            projectEntity.Property<DateTime>((Project project) => project.CreatedAt)
                .HasDefaultValueSql<DateTime>("getdate()");
        });

        modelBuilder.Entity<ProjectType>((EntityTypeBuilder<ProjectType> projectTypeEntity) =>
        {
            projectTypeEntity.ToTable("project_type");
            projectTypeEntity.HasKey((ProjectType projectType) => projectType.Id);
            projectTypeEntity.HasIndex((ProjectType projectType) => projectType.Name).IsUnique();
        });
    }
}
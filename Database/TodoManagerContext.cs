using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoManager.Models.Database;

namespace TodoManager.Database;

public class TodoManagerContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Log> Logs { get; set; }

    public TodoManagerContext(DbContextOptions<TodoManagerContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>((EntityTypeBuilder<User> userEntity) =>
        {
            userEntity.ToTable("user");

            userEntity.Property<int>((User user) => user.Id).HasColumnName<int>("id");
            userEntity.Property<string>((User user) => user.Name).HasColumnName<string>("name");
            userEntity.Property<string>((User user) => user.Login).HasColumnName<string>("login");
            userEntity.Property<string>((User user) => user.Password).HasColumnName<string>("password");

            userEntity.Property<DateTime>((User user) => user.CreatedAt)
                .HasColumnName<DateTime>("created_at")
                .HasDefaultValueSql<DateTime>("getdate()");

            userEntity.Property<DateTime?>((User user) => user.UpdatedAt).HasColumnName<DateTime?>("updated_at");

            userEntity.HasKey((User user) => user.Id);
            userEntity.HasIndex((User user) => user.Login).IsUnique();
        });

        modelBuilder.Entity<Project>((EntityTypeBuilder<Project> projectEntity) =>
        {
            projectEntity.ToTable("project");

            projectEntity.Property<int>((Project project) => project.Id).HasColumnName<int>("id");
            projectEntity.Property<string>((Project project) => project.Name).HasColumnName<string>("name");

            projectEntity.Property<DateTime>((Project project) => project.CreatedAt)
                .HasColumnName<DateTime>("created_at")
                .HasDefaultValueSql<DateTime>("getdate()");

            projectEntity.Property<DateTime?>((Project project) => project.UpdatedAt).HasColumnName<DateTime?>("updated_at");

            projectEntity.HasKey((Project project) => project.Id);
        });

        modelBuilder.Entity<Log>((EntityTypeBuilder<Log> logEntity) =>
        {
            logEntity.ToTable("log");

            logEntity.Property<int>((Log log) => log.Id).HasColumnName<int>("id");
            logEntity.Property<string>((Log log) => log.Endpoint).HasColumnName<string>("endpoint");
            logEntity.Property<string>((Log log) => log.Method).HasColumnName<string>("method");
            logEntity.Property<string>((Log log) => log.Header).HasColumnName<string>("header");
            logEntity.Property<string>((Log log) => log.Body).HasColumnName<string>("body");

            logEntity.Property<DateTime>((Log log) => log.CreatedAt)
                .HasColumnName<DateTime>("created_at")
                .HasDefaultValueSql<DateTime>("getdate()");

            logEntity.HasKey((Log log) => log.Id);
        });
    }
}
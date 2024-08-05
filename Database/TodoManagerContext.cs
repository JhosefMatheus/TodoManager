using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoManager.Models.Database;

namespace TodoManager.Database;

public class TodoManagerContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public TodoManagerContext(DbContextOptions<TodoManagerContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>((EntityTypeBuilder<User> userEntity) =>
        {
            userEntity.ToTable("user");
            userEntity.HasKey((User user) => user.Id);
            userEntity.HasIndex((User user) => user.Login).IsUnique();
            userEntity.Property((User user) => user.CreatedAt).HasDefaultValueSql("getdate()");
        });
    }
}
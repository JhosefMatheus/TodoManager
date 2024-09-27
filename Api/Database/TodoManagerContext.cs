using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Api.Models.Database;

namespace Api.Database;

public class TodoManagerContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ExceptionLog> ExceptionLogs { get; set; }
    public DbSet<ProjectSection> ProjectSections { get; set; }

    public TodoManagerContext(DbContextOptions<TodoManagerContext> options) : base(options) { }

    public static DbContextOptions<TodoManagerContext> CreateDbContextOptions()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.Development.json")
             .Build();

        DbContextOptionsBuilder<TodoManagerContext> optionsBuilder = new DbContextOptionsBuilder<TodoManagerContext>();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("TodoManager"));

        DbContextOptions<TodoManagerContext> options = optionsBuilder.Options;

        return options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>((EntityTypeBuilder<User> userEntity) =>
        {
            userEntity.ToTable<User>("user");

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
            projectEntity.ToTable<Project>("project");

            projectEntity.Property<int>((Project project) => project.Id).HasColumnName<int>("id");
            projectEntity.Property<string>((Project project) => project.Name).HasColumnName<string>("name");

            projectEntity.Property<bool>((Project project) => project.Archived)
                .HasColumnName<bool>("archived")
                .HasDefaultValue<bool>(false);

            projectEntity.Property<DateTime>((Project project) => project.CreatedAt)
                .HasColumnName<DateTime>("created_at")
                .HasDefaultValueSql<DateTime>("getdate()");

            projectEntity.Property<DateTime?>((Project project) => project.UpdatedAt).HasColumnName<DateTime?>("updated_at");

            projectEntity.HasKey((Project project) => project.Id);
        });

        modelBuilder.Entity<ExceptionLog>((EntityTypeBuilder<ExceptionLog> exceptionLogEntity) =>
        {
            exceptionLogEntity.ToTable<ExceptionLog>("exception_log");

            exceptionLogEntity.Property<int>((ExceptionLog exceptionLog) => exceptionLog.Id)
                .HasColumnName<int>("id");

            exceptionLogEntity.Property<string>((ExceptionLog exceptionLog) => exceptionLog.Endpoint)
                .HasColumnName<string>("endpoint");

            exceptionLogEntity.Property<string>((ExceptionLog exceptionLog) => exceptionLog.Method)
                .HasColumnName<string>("method");

            exceptionLogEntity.Property<string>((ExceptionLog exceptionLog) => exceptionLog.Header)
                .HasColumnName<string>("header")
                .HasColumnType<string>("nvarchar(max)");

            exceptionLogEntity.Property<string>((ExceptionLog exceptionLog) => exceptionLog.Body)
                .HasColumnName<string>("body")
                .HasColumnType<string>("nvarchar(max)");

            exceptionLogEntity.Property<string>((ExceptionLog exceptionLog) => exceptionLog.ErrorMessage)
                .HasColumnName<string>("error_message")
                .HasColumnType<string>("nvarchar(max)");

            exceptionLogEntity.Property<string>((ExceptionLog exceptionLog) => exceptionLog.StackTrace)
                .HasColumnName<string>("stack_trace")
                .HasColumnType<string>("nvarchar(max)");

            exceptionLogEntity.Property<DateTime>((ExceptionLog exceptionLog) => exceptionLog.CreatedAt)
                .HasColumnName<DateTime>("created_at")
                .HasDefaultValueSql<DateTime>("getdate()");

            exceptionLogEntity.HasKey((ExceptionLog exceptionLog) => exceptionLog.Id);
        });

        modelBuilder.Entity<ProjectSection>((EntityTypeBuilder<ProjectSection> projectSectionEntity) =>
        {
            projectSectionEntity.ToTable<ProjectSection>("project_section");

            projectSectionEntity.Property<int>((ProjectSection projectSection) => projectSection.Id)
                .HasColumnName<int>("id");

            projectSectionEntity.Property<int>((ProjectSection projectSection) => projectSection.ProjectId)
                .HasColumnName<int>("project_id");

            projectSectionEntity.Property<string>((ProjectSection projectSection) => projectSection.Name)
                .HasColumnName<string>("name");

            projectSectionEntity.Property<DateTime>((ProjectSection projectSection) => projectSection.CreatedAt)
                .HasColumnName<DateTime>("created_at")
                .HasDefaultValueSql<DateTime>("getdate()");

            projectSectionEntity.Property<DateTime?>((ProjectSection projectSection) => projectSection.UpdatedAt)
                .HasColumnName<DateTime?>("updated_at");

            projectSectionEntity.HasKey((ProjectSection projectSection) => projectSection.Id);

            projectSectionEntity.HasOne<Project>((ProjectSection projectSection) => projectSection.Project)
                .WithMany((Project project) => project.ProjectSections)
                .HasForeignKey((ProjectSection projectSection) => projectSection.ProjectId);
        });
    }
}
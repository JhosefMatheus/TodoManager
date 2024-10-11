using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Api.Models.Database;

namespace Api.Database;

public class TodoManagerContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ExceptionLogEntity> ExceptionLogs { get; set; }
    public DbSet<ProjectSectionEntity> ProjectSections { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<TaskTypeEntity> TaskTypes { get; set; }
    public DbSet<TaskDayEntity> TaskDays { get; set; }

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
        modelBuilder.Entity<UserEntity>((EntityTypeBuilder<UserEntity> userEntity) =>
        {
            userEntity.ToTable<UserEntity>("user");

            userEntity.Property<int>((UserEntity user) => user.Id).HasColumnName<int>("id");
            userEntity.Property<string>((UserEntity user) => user.Name).HasColumnName<string>("name");
            userEntity.Property<string>((UserEntity user) => user.Login).HasColumnName<string>("login");
            userEntity.Property<string>((UserEntity user) => user.Password).HasColumnName<string>("password");

            userEntity.Property<DateTime>((UserEntity user) => user.CreatedAt)
                .HasColumnName<DateTime>("created_at")
                .HasDefaultValueSql<DateTime>("getdate()");

            userEntity.Property<DateTime?>((UserEntity user) => user.UpdatedAt).HasColumnName<DateTime?>("updated_at");

            userEntity.HasKey((UserEntity user) => user.Id);
            userEntity.HasIndex((UserEntity user) => user.Login).IsUnique();
        });

        modelBuilder.Entity<ProjectEntity>((EntityTypeBuilder<ProjectEntity> projectEntity) =>
        {
            projectEntity.ToTable<ProjectEntity>("project");

            projectEntity.Property<int>((ProjectEntity project) => project.Id).HasColumnName<int>("id");
            projectEntity.Property<string>((ProjectEntity project) => project.Name).HasColumnName<string>("name");

            projectEntity.Property<bool>((ProjectEntity project) => project.Archived)
                .HasColumnName<bool>("archived")
                .HasDefaultValue<bool>(false);

            projectEntity.Property<DateTime>((ProjectEntity project) => project.CreatedAt)
                .HasColumnName<DateTime>("created_at")
                .HasDefaultValueSql<DateTime>("getdate()");

            projectEntity.Property<DateTime?>((ProjectEntity project) => project.UpdatedAt).HasColumnName<DateTime?>("updated_at");

            projectEntity.HasKey((ProjectEntity project) => project.Id);
        });

        modelBuilder.Entity<ExceptionLogEntity>((EntityTypeBuilder<ExceptionLogEntity> exceptionLogEntity) =>
        {
            exceptionLogEntity.ToTable<ExceptionLogEntity>("exception_log");

            exceptionLogEntity.Property<int>((ExceptionLogEntity exceptionLog) => exceptionLog.Id)
                .HasColumnName<int>("id");

            exceptionLogEntity.Property<string>((ExceptionLogEntity exceptionLog) => exceptionLog.Endpoint)
                .HasColumnName<string>("endpoint");

            exceptionLogEntity.Property<string>((ExceptionLogEntity exceptionLog) => exceptionLog.Method)
                .HasColumnName<string>("method");

            exceptionLogEntity.Property<string>((ExceptionLogEntity exceptionLog) => exceptionLog.Header)
                .HasColumnName<string>("header")
                .HasColumnType<string>("nvarchar(max)");

            exceptionLogEntity.Property<string>((ExceptionLogEntity exceptionLog) => exceptionLog.Body)
                .HasColumnName<string>("body")
                .HasColumnType<string>("nvarchar(max)");

            exceptionLogEntity.Property<string>((ExceptionLogEntity exceptionLog) => exceptionLog.ErrorMessage)
                .HasColumnName<string>("error_message")
                .HasColumnType<string>("nvarchar(max)");

            exceptionLogEntity.Property<string>((ExceptionLogEntity exceptionLog) => exceptionLog.StackTrace)
                .HasColumnName<string>("stack_trace")
                .HasColumnType<string>("nvarchar(max)");

            exceptionLogEntity.Property<DateTime>((ExceptionLogEntity exceptionLog) => exceptionLog.CreatedAt)
                .HasColumnName<DateTime>("created_at")
                .HasDefaultValueSql<DateTime>("getdate()");

            exceptionLogEntity.HasKey((ExceptionLogEntity exceptionLog) => exceptionLog.Id);
        });

        modelBuilder.Entity<ProjectSectionEntity>((EntityTypeBuilder<ProjectSectionEntity> projectSectionEntity) =>
        {
            projectSectionEntity.ToTable<ProjectSectionEntity>("project_section");

            projectSectionEntity.Property<int>((ProjectSectionEntity projectSection) => projectSection.Id)
                .HasColumnName<int>("id");

            projectSectionEntity.Property<int>((ProjectSectionEntity projectSection) => projectSection.ProjectId)
                .HasColumnName<int>("project_id");

            projectSectionEntity.Property<string>((ProjectSectionEntity projectSection) => projectSection.Name)
                .HasColumnName<string>("name");

            projectSectionEntity.Property<bool>((ProjectSectionEntity projectSection) => projectSection.Archived)
                .HasColumnName<bool>("archived")
                .HasDefaultValue<bool>(false);

            projectSectionEntity.Property<DateTime>((ProjectSectionEntity projectSection) => projectSection.CreatedAt)
                .HasColumnName<DateTime>("created_at")
                .HasDefaultValueSql<DateTime>("getdate()");

            projectSectionEntity.Property<DateTime?>((ProjectSectionEntity projectSection) => projectSection.UpdatedAt)
                .HasColumnName<DateTime?>("updated_at");

            projectSectionEntity.HasKey((ProjectSectionEntity projectSection) => projectSection.Id);

            projectSectionEntity.HasOne<ProjectEntity>((ProjectSectionEntity projectSection) => projectSection.Project)
                .WithMany((ProjectEntity project) => project.ProjectSections)
                .HasForeignKey((ProjectSectionEntity projectSection) => projectSection.ProjectId);
        });

        modelBuilder.Entity<TaskEntity>((EntityTypeBuilder<TaskEntity> taskEntity) =>
        {
            taskEntity.ToTable<TaskEntity>("task");

            taskEntity.Property<int>((TaskEntity task) => task.Id).HasColumnName<int>("id");

            taskEntity.Property<int?>((TaskEntity task) => task.ProjectId).HasColumnName<int?>("project_id");

            taskEntity.Property<int?>((TaskEntity task) => task.ProjectSectionId).HasColumnName<int?>("project_section_id");

            taskEntity.Property<int>((TaskEntity task) => task.TaskTypeId).HasColumnName<int>("task_type_id");

            taskEntity.Property<string>((TaskEntity task) => task.Name).HasColumnName<string>("name");

            taskEntity.Property<string?>((TaskEntity task) => task.Description).HasColumnName<string?>("description");

            taskEntity.Property<bool>((TaskEntity task) => task.Archived).HasColumnName<bool>("archived").HasDefaultValue<bool>(false);

            taskEntity.Property<DateTime>((TaskEntity task) => task.CreatedAt).HasColumnName<DateTime>("created_at")
                .HasDefaultValueSql<DateTime>("getdate()");

            taskEntity.Property<DateTime?>((TaskEntity task) => task.UpdatedAt).HasColumnName<DateTime?>("updated_at");

            taskEntity.HasKey((TaskEntity task) => task.Id);

            taskEntity.HasOne<ProjectEntity>((TaskEntity task) => task.Project).WithMany((ProjectEntity project) => project.Tasks)
                .HasForeignKey((TaskEntity task) => task.ProjectId);

            taskEntity.HasOne<ProjectSectionEntity>((TaskEntity task) => task.ProjectSection)
                .WithMany((ProjectSectionEntity projectSection) => projectSection.Tasks)
                .HasForeignKey((TaskEntity task) => task.ProjectSectionId);

            taskEntity.HasOne<TaskTypeEntity>((TaskEntity task) => task.TaskType)
                .WithMany((TaskTypeEntity taskType) => taskType.Tasks)
                .HasForeignKey((TaskEntity task) => task.TaskTypeId);
        });

        modelBuilder.Entity<TaskTypeEntity>((EntityTypeBuilder<TaskTypeEntity> taskTypeEntity) =>
        {
            taskTypeEntity.ToTable<TaskTypeEntity>("task_type");

            taskTypeEntity.Property<int>((TaskTypeEntity taskType) => taskType.Id).HasColumnName<int>("id");

            taskTypeEntity.Property<string>((TaskTypeEntity taskType) => taskType.Name).HasColumnName<string>("name");

            taskTypeEntity.HasKey((TaskTypeEntity taskType) => taskType.Id);
        });

        modelBuilder.Entity<TaskDayEntity>((EntityTypeBuilder<TaskDayEntity> taskDayEntity) =>
        {
            taskDayEntity.ToTable<TaskDayEntity>("task_day");

            taskDayEntity.Property<int>((TaskDayEntity taskDay) => taskDay.TaskId).HasColumnName<int>("task_id");

            taskDayEntity.Property<int>((TaskDayEntity taskDay) => taskDay.Day).HasColumnName<int>("day");

            taskDayEntity.Property<DateTime>((TaskDayEntity taskDay) => taskDay.CreatedAt).HasColumnName<DateTime>("created_at")
                .HasDefaultValueSql<DateTime>("getdate()");

            taskDayEntity.Property<DateTime?>((TaskDayEntity taskDay) => taskDay.UpdatedAt).HasColumnName<DateTime?>("updated_at");

            taskDayEntity.HasKey((TaskDayEntity taskDay) => new { taskDay.TaskId, taskDay.Day });

            taskDayEntity.HasOne<TaskEntity>((TaskDayEntity taskDay) => taskDay.Task)
                .WithMany((TaskEntity task) => task.Days)
                .HasForeignKey((TaskDayEntity taskDay) => taskDay.TaskId);
        });
    }
}
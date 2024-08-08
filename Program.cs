using Microsoft.EntityFrameworkCore;
using TodoManager.Database;
using TodoManager.Services;

namespace TodoManager;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddDbContext<TodoManagerContext>((DbContextOptionsBuilder options) =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("TodoManager"));
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<ProjectService>();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

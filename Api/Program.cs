using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Database;
using Api.Filters.Exceptions;
using Api.Services;
using Api.Services.Filters.Exceptions;
using Api.Services.Loggers.Exceptions.Filters;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers((MvcOptions options) =>
        {
            options.Filters.Add<ExceptionFilter>();
        });

        builder.Services.AddDbContext<TodoManagerContext>((DbContextOptionsBuilder options) =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("TodoManager"));
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<ExceptionFilterLoggerService>();

        builder.Services.AddScoped<ExceptionFilterService>();
        builder.Services.AddScoped<BaseExceptionFilterService>();
        builder.Services.AddScoped<BaseHttpExceptionFilterService>();

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
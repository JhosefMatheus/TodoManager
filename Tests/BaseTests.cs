using Api.Database;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public abstract class BaseTests
{
    protected readonly ServiceCollection services;
    protected readonly ServiceProvider serviceProvider;

    public BaseTests()
    {
        services = new ServiceCollection();

        services.AddDbContext<TodoManagerContext>(options =>
        {
            options
                .UseInMemoryDatabase("TestTodoManagerDatabase")
                .ConfigureWarnings((WarningsConfigurationBuilder warningsConfigurationBuilder) =>
                {
                    warningsConfigurationBuilder.Ignore(InMemoryEventId.TransactionIgnoredWarning);
                });
        });

        services.AddScoped<ProjectSectionService>();
        services.AddScoped<ProjectService>();
        services.AddScoped<TaskTypeService>();

        serviceProvider = services.BuildServiceProvider();
    }
}
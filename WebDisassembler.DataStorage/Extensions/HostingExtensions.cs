using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebDisassembler.DataStorage.Options;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.DataStorage.Repositories.Impl;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Extensions;

public static class HostingExtensions
{
    public static void AddDataStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DataStorageOptions>(o =>
        {
            configuration.GetSection("DataStorage")
                .Bind(o);
        });
        services.AddDbContext<DatabaseContext>();

        services.AddScoped<IFileReferenceRepository, FileReferenceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IBinaryRepository, BinaryRepository>();
    }

    public static void ExecuteMigrations(this IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            context.Database.Migrate();
        }
    }
}

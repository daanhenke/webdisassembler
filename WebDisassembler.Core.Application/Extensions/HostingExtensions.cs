using Microsoft.Extensions.DependencyInjection;
using WebDisassembler.Core.Application.Services;
using WebDisassembler.Core.Application.Services.Admin;
using WebDisassembler.Core.Mapping;

namespace WebDisassembler.Core.Extensions;

public static class HostingExtensions
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<FileService>();

        services.AddScoped<SearchService>();
        services.AddScoped<TenantAdminService>();

        services.AddScoped<TenantService>();

        services.AddScoped<AuthenticationService>();
        services.AddScoped<ProjectService>();
        services.AddScoped<BinaryService>();
    }
}
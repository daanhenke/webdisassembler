using Microsoft.Extensions.DependencyInjection;
using WebDisassembler.Core.Application.Services;
using WebDisassembler.Core.Identity;
using WebDisassembler.Core.Mapping;

namespace WebDisassembler.Core.Extensions;

public static class HostingExtensions
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<ProjectService>();
        services.AddScoped<BinaryService>();
        services.AddScoped<FileService>();
        services.AddScoped<IdentityDetails>();
    }
}
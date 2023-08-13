using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebDisassembler.Search.Client.Indices;
using WebDisassembler.Search.Client.Indices.Impl;
using WebDisassembler.Search.Data.Extensions;

namespace WebDisassembler.Search.Client.Extensions;

public static class HostingExtensions
{
    public static void AddElasticClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddElasticClient(configuration);
        services.AddScoped<ITenantIndex, TenantIndex>();
        services.AddScoped<IUserIndex, UserIndex>();
        services.AddScoped<IProjectIndex, ProjectIndex>();
    }
}

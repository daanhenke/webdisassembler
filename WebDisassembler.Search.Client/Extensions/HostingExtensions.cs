using Microsoft.Extensions.DependencyInjection;
using WebDisassembler.Search.Client.Indices;
using WebDisassembler.Search.Client.Indices.Impl;
using WebDisassemlber.Search.Data.Utility;

namespace WebDisassembler.Search.Client.Extensions;

public static class HostingExtensions
{
    public static void AddElasticClients(this IServiceCollection services)
    {
        services.AddScoped<ElasticSearchClient>();
        services.AddScoped<ITenantIndex, TenantIndex>();
        services.AddScoped<IUserIndex, UserIndex>();
        services.AddScoped<IProjectIndex, ProjectIndex>();
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebDisassembler.Search.Data.Options;

namespace WebDisassembler.Search.Data.Extensions;

public static class HostingExtensions
{
    public static void AddElasticClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ElasticSearchOptions>(o =>
        {
            configuration.GetSection("ElasticSearch")
                .Bind(o);
        });
    }
}
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WebDisassembler.Core.Logging.Extensions;

public static class HostingExtensions
{
    public static void AddLogging(this IServiceCollection services, IConfiguration configuration)
    {
        var elasticCfg = configuration.GetSection("ElasticSearch");
        var protocol = elasticCfg["Protocol"] ?? "https";
        var host = elasticCfg["Host"] ?? "localhost:";
            
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Elasticsearch(new [] { new Uri($"{protocol}://elastic:password@{host}", UriKind.Absolute) },
            opts =>
            {
                opts.BootstrapMethod = BootstrapMethod.Failure;
                opts.DataStream = new DataStreamName("logs", "api", "dev");
            }, transport =>
            {
                transport.ServerCertificateValidationCallback(CertificateValidations.AllowAll);
            })
            .WriteTo.Console()
            .CreateLogger();
        
        services.AddLogging(loggingBuilder => loggingBuilder
            .ClearProviders()
            .AddSerilog(logger));

        services.AddSerilog(logger);
    }
}
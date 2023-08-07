using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebDisassembler.Core.ServiceProtocol.Options;
using WebDisassembler.Core.ServiceProtocol.Utility;

namespace WebDisassembler.Core.ServiceProtocol.Extensions;

public static class HostingExtensions
{
    public static void AddServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new ServiceProtocolOptions();
        configuration.GetSection("ServiceProtocol")
            .Bind(options);

        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => assembly.GetName().Name.StartsWith("WebDisassembler."))
            .ToArray();

        services.AddMassTransit(c =>
        {
            c.AddConsumers(assemblies);
            c.UsingRabbitMq((ctx, rq) =>
            {
                rq.Host(options.Host, options.Path, h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });
                
                rq.ConfigureEndpoints(ctx);
            });
        });

        var serviceTypes = assemblies.SelectMany(assembly => assembly.ExportedTypes)
            .Where(type => type.BaseType == typeof(ServiceClientBase))
            .ToHashSet();

        foreach (var implementationType in serviceTypes)
        {
            var interfaceType = implementationType
                .GetInterfaces()
                .First(iface => iface.Name == $"I{implementationType.Name}");

            services.AddScoped(interfaceType, implementationType);
        }
    }
}
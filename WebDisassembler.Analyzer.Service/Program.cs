
using WebDisassembler.ServiceProtocol.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddServiceBus(ctx.Configuration);
    })
    .Build();

host.Run();
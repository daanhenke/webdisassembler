using WebDisassembler.Analyzer.Core.Pipeline;
using WebDisassembler.Core.ServiceProtocol.Extensions;
using WebDisassembler.DataStorage.Extensions;
using WebDisassembler.FileStorage.Extensions;
using WebDisassembler.Loader.PortableExecutable;

var lol = new HashSet<Type>()
{
    typeof(PortableExecutableLoader)
};

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddServiceBus(ctx.Configuration);
        services.AddDataStorage(ctx.Configuration);
        services.AddFileStorage(ctx.Configuration);

        services.AddScoped<AnalysisPipeline>();
    })
    .Build();

host.Run();
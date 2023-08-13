using WebDisassembler.Core.ServiceProtocol.Extensions;
using WebDisassembler.DataStorage.Extensions;
using WebDisassemlber.Search.Data.Utility;
using WebDisassembler.Search.Service.Indexers;
using System.Reflection;
using WebDisassembler.Search.Data.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddServiceBus(ctx.Configuration);
        services.AddDataStorage(ctx.Configuration);

        services.AddElasticClient(ctx.Configuration);
        services.AddScoped<UserIndexer>();
        services.AddScoped<TenantIndexer>();
        services.AddScoped<ProjectIndexer>();
    })
    .Build();

host.Run();
using AutoMapper;
using WebDisassembler.ServiceProtocol.Extensions;
using WebDisassembler.DataStorage.Extensions;
using WebDisassemlber.Search.Data.Utility;
using WebDisassembler.Search.Service.Indexers;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddServiceBus(ctx.Configuration);
        services.AddDataStorage(ctx.Configuration);

        services.AddScoped<ElasticSearchClient>();
        services.AddScoped<UserIndexer>();
        services.AddScoped<TenantIndexer>();
    })
    .Build();

host.Run();
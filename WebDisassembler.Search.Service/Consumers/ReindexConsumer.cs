using MassTransit;
using WebDisassembler.Search.Service.Indexers;
using WebDisassembler.ServiceProtocol.Contracts;

namespace WebDisassembler.Search.Service.Consumers;

public class ReindexConsumer :
    IConsumer<IndexAllRecordsRequest>,
    IConsumer<IndexUsersRequest>,
    IConsumer<IndexTenantsRequest>
{
    private readonly UserIndexer _userIndexer;
    private readonly TenantIndexer _tenantIndexer;

    public ReindexConsumer(UserIndexer userIndexer, TenantIndexer tenantIndexer)
    {
        _userIndexer = userIndexer;
        _tenantIndexer = tenantIndexer;
    }

    public async Task Consume(ConsumeContext<IndexAllRecordsRequest> context)
    {
        var indices = context.Message.Indices.Select(index => $"indexed{index.ToLowerInvariant()}");

        if (indices.Contains(_userIndexer.IndexName))
        {
            await _userIndexer.RecreateIndex();
        }

        if (indices.Contains(_tenantIndexer.IndexName))
        {
            await _tenantIndexer.RecreateIndex();
        }

        await context.RespondAsync<IndexAllRecordsResponse>(new());
    }

    public async Task Consume(ConsumeContext<IndexUsersRequest> context)
    {
        await _userIndexer.IndexEntities(context.Message.UserIds.ToHashSet());
        await context.RespondAsync<IndexUsersResponse>(new());
    }

    public async Task Consume(ConsumeContext<IndexTenantsRequest> context)
    {
        await _tenantIndexer.IndexEntities(context.Message.TenantIds.ToHashSet());
        await context.RespondAsync<IndexTenantsResponse>(new());
    }

}

using AutoMapper;
using WebDisassembler.Core.Common.Models;
using WebDisassemlber.Search.Data.Utility;

namespace WebDisassembler.Search.Service.Utility;

public abstract class IndexerBase<TModel> where TModel : class
{
    private readonly ElasticSearchClient _client;
    protected readonly IMapper _mapper;

    protected IndexerBase(ElasticSearchClient client, IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }

    public string IndexName
        => _client.IndexName<TModel>();

    public async ValueTask IndexEntities(ISet<Guid> ids)
    {
        if (! await _client.DoesIndexExist<TModel>())
        {
            await _client.CreateIndex<TModel>();
        }

        var models = await FetchFromDatabase(ids);
        await _client.IndexModels(models);
    }



    public async ValueTask RecreateIndex()
    {
        if (await _client.DoesIndexExist<TModel>())
        {
            await _client.DeleteIndex<TModel>();
        }
        await _client.CreateIndex<TModel>();

        var request = new PagedRequest(0, 50);
        while (true)
        {
            var response = await FetchAllFromDatabase(request);

            if (response.Items.Length == 0)
            {
                break;
            }

            await _client.IndexModels(response.Items);
            request = request with { Index = request.Index + 1 };
        }
    }

    protected abstract ValueTask<IReadOnlyCollection<TModel>> FetchFromDatabase(ISet<Guid> ids);
    protected abstract ValueTask<PagedResponse<TModel>> FetchAllFromDatabase(PagedRequest request);
}

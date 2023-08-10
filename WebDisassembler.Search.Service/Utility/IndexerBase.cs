using AutoMapper;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.Mapping;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Search.Data.Utility;
using WebDisassemlber.Search.Data.Utility;

namespace WebDisassembler.Search.Service.Utility;

public abstract class IndexerBase<TSourceModel, TIndexModel>
    where TSourceModel : class
    where TIndexModel : class, IIndexedEntity
{
    private readonly ElasticSearchClient _client;
    protected readonly IMapper _mapper;

    protected IndexerBase(ElasticSearchClient client, IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }

    public string IndexName
        => _client.IndexName<TIndexModel>();

    public async ValueTask IndexEntities(ISet<Guid> ids)
    {
        if (! await _client.DoesIndexExist<TIndexModel>())
        {
            await _client.CreateIndex<TIndexModel>(SetIndexMappingInternal);
        }

        var models = (await FetchFromDatabase(ids))
            .Select(Map)
            .ToHashSet();

        await _client.IndexModels(models);
    }



    public async ValueTask RecreateIndex()
    {
        if (await _client.DoesIndexExist<TIndexModel>())
        {
            await _client.DeleteIndex<TIndexModel>();
        }
        await _client.CreateIndex<TIndexModel>(SetIndexMappingInternal);

        var request = new PagedRequest(0, 50);
        while (true)
        {
            var response = await FetchAllFromDatabase(request);

            if (response.Items.Length == 0)
            {
                break;
            }

            await _client.IndexModels(response.Items.Select(Map).ToHashSet());
            request = request with { Index = request.Index + 1 };
        }
    }

    protected virtual TIndexModel Map(TSourceModel model)
        => _mapper.Map<TIndexModel>(model);

    private void SetIndexMappingInternal(TypeMappingDescriptor<TIndexModel> descriptor)
    {
        descriptor.Properties(p =>
        {
            p.Keyword(m => m.Id, m => m.Index());
            SetIndexMapping(p);
        });
    }
    
    protected virtual void SetIndexMapping(PropertiesDescriptor<TIndexModel> descriptor) {}

    protected abstract ValueTask<IReadOnlyCollection<TSourceModel>> FetchFromDatabase(ISet<Guid> ids);
    protected abstract ValueTask<PagedResponse<TSourceModel>> FetchAllFromDatabase(PagedRequest request);
}

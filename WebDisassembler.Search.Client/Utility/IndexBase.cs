using Elastic.Clients.Elasticsearch.QueryDsl;
using WebDisassembler.Search.Data.Utility;
using WebDisassemlber.Search.Data.Utility;

namespace WebDisassembler.Search.Client.Utility;

public abstract class IndexBase<TModel> : IIndex<TModel> where TModel : class, IIndexedEntity
{
    protected readonly ElasticSearchClient _client;

    protected IndexBase(ElasticSearchClient client)
    {
        _client = client;
    }

    public async ValueTask<TModel> GetById(Guid id)
    {
        return await _client.GetById<TModel>(id);
    }
}
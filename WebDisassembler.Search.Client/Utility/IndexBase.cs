using WebDisassemlber.Search.Data.Utility;

namespace WebDisassembler.Search.Client.Utility;

public abstract class IndexBase<TModel> : IIndex<TModel> where TModel : class
{
    protected readonly ElasticSearchClient _client;

    protected IndexBase(ElasticSearchClient client)
    {
        _client = client;
    }
}

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Search.Data.Utility;

namespace WebDisassemlber.Search.Data.Utility;

public class ElasticSearchClient
{
    private readonly ElasticsearchClient _client;

    public ElasticSearchClient()
    {

        var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9201"))
            .DisableDirectStreaming();
        
        _client = new(settings);
    }

    public async ValueTask CreateIndex<TModel>(Action<TypeMappingDescriptor<TModel>> mapCallback) where TModel : class
    {
        var response = await _client.Indices.CreateAsync<TModel>(d => d
            .Index(IndexName<TModel>())
            .Mappings(m => mapCallback(m))
        );

        if (! response.IsSuccess())
        {
            throw new NotSupportedException();
        }
    }

    public async Task DeleteIndex<TModel>() where TModel : class
    {
        var response = await _client.Indices.DeleteAsync(IndexName<TModel>());

        if (! response.IsSuccess())
        {
            throw new NotSupportedException();
        }
    }

    public async ValueTask<bool> DoesIndexExist<TModel>() where TModel : class
    {
        var response = await _client.Indices.ExistsAsync(IndexName<TModel>());

        return response.Exists;
    }

    public async ValueTask IndexModels<TModel>(IReadOnlyCollection<TModel> models) where TModel : class
    {
        var response = await _client.IndexManyAsync(models, IndexName<TModel>());

        if (! response.IsSuccess())
        {
            throw new NotSupportedException();
        }
    }

    public async ValueTask<PagedResponse<TModel>> FindPaged<TModel>(PagedRequest pagedRequest, Action<SearchRequestDescriptor<TModel>> queryCallback) where TModel : class
    {
        var response = await FindInternal<TModel>(query =>
        {
            query
                .From(pagedRequest.Index * pagedRequest.Size)
                .Size(pagedRequest.Size);

            queryCallback(query);
        });

        return new PagedResponse<TModel>((int) response.Total, response.Documents.ToArray());
    }

    public string IndexName<TModel>(string? suffix = null)  where TModel : class
        => suffix == null ? ModelName<TModel>() : $"{ModelName<TModel>()}_{suffix}";

    private string ModelName<TModel>() where TModel : class
        => typeof(TModel).Name.ToLowerInvariant();

    private async ValueTask<SearchResponse<TModel>> FindInternal<TModel>(Action<SearchRequestDescriptor<TModel>> queryCallback) where TModel : class
    {
        var response = await _client.SearchAsync<TModel>(q =>
        {
            q.Index(IndexName<TModel>());
            queryCallback(q);
        });

        if (! response.IsSuccess())
        {
            throw new NotSupportedException();
        }

        return response;
    }

    public async ValueTask<TModel> GetById<TModel>(Guid id) where TModel : class, IIndexedEntity
    {
        var response = await FindInternal<TModel>(q => q
            .Query(q => q
                .Term(m => m.Id, id)
            )
        );

        return response.Documents.First();
    }
}

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Options;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Search.Data.Options;
using WebDisassembler.Search.Data.Utility;

namespace WebDisassemlber.Search.Data.Utility;

public class ElasticSearchClient
{
    private readonly ElasticsearchClient _client;
    private readonly IOptions<ElasticSearchOptions> _options;

    public ElasticSearchClient(IOptions<ElasticSearchOptions> options)
    {
        _options = options;

        var settings = new ElasticsearchClientSettings(new Uri(_options.Value.Host))
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
        var request = new BulkRequest(IndexName<TModel>())
        {
            Operations = new BulkOperationsCollection(
                models
                    .Select(m => new BulkIndexOperation<TModel>(m))
                    .ToArray()
            ),
            Refresh = Refresh.WaitFor
        };

        var response = await _client.BulkAsync(request);

        if (! response.IsSuccess())
        {
            throw new NotSupportedException();
        }
    }

    public async ValueTask<PagedResponse<TModel>> FindPaged<TModel>(PagedRequest request, Action<QueryDescriptor<TModel>> queryCallback) where TModel : class
    {
        var response = await FindInternal<TModel>(searchRequestDescriptor =>
        {
            searchRequestDescriptor
                .From(request.Index * request.Size)
                .Size(request.Size);

            searchRequestDescriptor.Query(queryCallback);
        });

        return new PagedResponse<TModel>((int) response.Total, response.Documents.ToArray());
    }
    
    public async ValueTask<PagedResponse<TModel>> FindPagedWithQuery<TModel>(QueryRequest request, Action<QueryDescriptor<TModel>> queryCallback) where TModel : class
    {
        var response = await FindInternal<TModel>(query =>
        {
            query
                .From(request.Index * request.Size)
                .Size(request.Size)
                .Query(queryDescriptor =>
                {
                    queryDescriptor.QueryString(qs => qs.Query(
                        string.IsNullOrWhiteSpace(request.Query)
                            ? "*"
                            : $"*{request.Query}*"
                    ));

                    queryCallback(queryDescriptor);
                });
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

using WebDisassembler.Core.Common.Models;
using WebDisassembler.Search.Client.Utility;
using WebDisassemlber.Search.Data.Models;
using WebDisassemlber.Search.Data.Utility;

namespace WebDisassembler.Search.Client.Indices.Impl;

public class TenantIndex : IndexBase<IndexedTenant>, ITenantIndex
{
    public TenantIndex(ElasticSearchClient client) : base(client) {}

    public ValueTask<PagedResponse<IndexedTenant>> FindPublic(PagedRequest request)
    {
        return _client.FindPaged<IndexedTenant>(request, q => q
            .Term(t => t.Public, true)
        );
    }

    public ValueTask<PagedResponse<IndexedTenant>> FindAll(QueryRequest request)
    {
        return _client.FindPagedWithQuery<IndexedTenant>(request, q => {});
    }
}

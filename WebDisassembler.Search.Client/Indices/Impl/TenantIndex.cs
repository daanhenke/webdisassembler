using WebDisassembler.Core.Common.Models;
using WebDisassembler.Search.Client.Utility;
using WebDisassembler.Search.Data.Utility;
using WebDisassemlber.Search.Data.Models;

namespace WebDisassembler.Search.Client.Indices.Impl;

public class TenantIndex : IndexBase<IndexedTenant>, ITenantIndex
{
    public TenantIndex(ElasticSearchClient client) : base(client) {}
    
    public ValueTask<PagedResponse<IndexedTenant>> FindForUser(Guid userId, PagedRequest request)
    {
        return _client.FindPaged<IndexedTenant>(request, q => q
            .Nested(n => n
                .Path(t => t.Users)
                .Query(q => q
                    .Term(t => t.Users.First().UserId, userId)             
                )
            )
        );
    }

    public ValueTask<PagedResponse<IndexedTenant>> FindAll(QueryRequest request)
    {
        return _client.FindPagedWithQuery<IndexedTenant>(request, q => {});
    }
}

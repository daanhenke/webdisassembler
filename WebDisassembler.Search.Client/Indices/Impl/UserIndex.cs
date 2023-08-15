using WebDisassembler.Core.Common.Models;
using WebDisassembler.Search.Client.Utility;
using WebDisassembler.Search.Data.Utility;
using WebDisassemlber.Search.Data.Models;

namespace WebDisassembler.Search.Client.Indices.Impl;

public class UserIndex : IndexBase<IndexedUser>, IUserIndex
{
    public UserIndex(ElasticSearchClient client) : base(client) {}

    public ValueTask<PagedResponse<IndexedUser>> FindAll(QueryRequest request)
    {
        return _client.FindPagedWithQuery<IndexedUser>(request, q => {});
    }
}

using WebDisassembler.Search.Client.Utility;
using WebDisassemlber.Search.Data.Models;
using WebDisassemlber.Search.Data.Utility;

namespace WebDisassembler.Search.Client.Indices.Impl;

public class UserIndex : IndexBase<IndexedUser>, IUserIndex
{
    public UserIndex(ElasticSearchClient client) : base(client) {}
}

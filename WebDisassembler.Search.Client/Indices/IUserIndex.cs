using WebDisassembler.Core.Common.Models;
using WebDisassembler.Search.Client.Utility;
using WebDisassemlber.Search.Data.Models;

namespace WebDisassembler.Search.Client.Indices;

public interface IUserIndex : IIndex<IndexedUser>
{
    ValueTask<PagedResponse<IndexedUser>> FindAll(QueryRequest request);
}

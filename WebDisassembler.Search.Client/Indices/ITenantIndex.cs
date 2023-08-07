using WebDisassembler.Core.Common.Models;
using WebDisassembler.Search.Client.Utility;
using WebDisassemlber.Search.Data.Models;

namespace WebDisassembler.Search.Client.Indices;

public interface ITenantIndex : IIndex<IndexedTenant>
{
    ValueTask<PagedResponse<IndexedTenant>> FindPublic(PagedRequest request);
}

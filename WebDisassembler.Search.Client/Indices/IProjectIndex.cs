using WebDisassembler.Core.Common.Models;
using WebDisassembler.Search.Client.Utility;
using WebDisassemlber.Search.Data.Models;

namespace WebDisassembler.Search.Client.Indices;

public interface IProjectIndex : IIndex<IndexedProject>
{
    ValueTask<PagedResponse<IndexedProject>> FindForUser(Guid userId, PagedRequest request);
}

using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Repositories;

public interface ITenantRepository : IRepository<Tenant>
{
    ValueTask<PagedResponse<Tenant>> GetAllForIndex(PagedRequest request);
}

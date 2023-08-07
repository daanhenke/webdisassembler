using Microsoft.EntityFrameworkCore;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Repositories.Impl;

public class TenantRepository : RepositoryBase<Tenant>, ITenantRepository
{
    public TenantRepository(DatabaseContext database) : base(database, () => database.Tenants) { }

    public async ValueTask<PagedResponse<Tenant>> GetAllForIndex(PagedRequest request)
    {
        return await Query()
            .ToPaged(request);
    }
}
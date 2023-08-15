using AutoMapper;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.Search.Data.Utility;
using WebDisassembler.Search.Service.Utility;
using WebDisassemlber.Search.Data.Models;

namespace WebDisassembler.Search.Service.Indexers;

public class TenantIndexer : IndexerBase<Tenant, IndexedTenant>
{
    private readonly ITenantRepository _tenantRepository;

    public TenantIndexer(ElasticSearchClient client, IMapper mapper, ITenantRepository tenantRepository) : base(client, mapper)
    {
        _tenantRepository = tenantRepository;
    }

    protected override async ValueTask<PagedResponse<Tenant>> FetchAllFromDatabase(PagedRequest request)
    {
        return await _tenantRepository.GetAllForIndex(request);
    }

    protected override async ValueTask<IReadOnlyCollection<Tenant>> FetchFromDatabase(ISet<Guid> ids)
    {
        return await _tenantRepository.GetMany(ids, false);
    }
}

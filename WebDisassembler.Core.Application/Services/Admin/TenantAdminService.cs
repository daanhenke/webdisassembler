using AutoMapper;
using WebDisassembler.Core.Application.Models;
using WebDisassembler.Core.Application.Models.Admin;
using WebDisassembler.Core.Application.Models.Identity;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.Core.ServiceProtocol.Clients;
using WebDisassembler.Search.Client.Indices;

namespace WebDisassembler.Core.Application.Services.Admin;

public class TenantAdminService
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IMapper _mapper;
    private readonly ISearchServiceClient _searchServiceClient;
    private readonly ITenantIndex _tenantIndex;

    public TenantAdminService(ITenantRepository tenantRepository, IMapper mapper, ISearchServiceClient searchServiceClient, ITenantIndex tenantIndex)
    {
        _tenantRepository = tenantRepository;
        _mapper = mapper;
        _searchServiceClient = searchServiceClient;
        _tenantIndex = tenantIndex;
    }

    public async ValueTask CreateDebugTenant(CreateTenant createTenant, Guid ownerId)
    {
        var tenant = _mapper.Map<Tenant>(createTenant);
        tenant.UserId = ownerId;

        tenant.Roles = new List<Role>()
        {
            new()
            {
                IsAdministrator = true,
                Name = "Administrators",
                Permissions = new HashSet<Permission>()
            }
        };

        _tenantRepository.Add(tenant);
        await _tenantRepository.Commit();

        tenant.TenantUsers = new List<TenantUser>()
        {
            new()
            {
                UserId = ownerId,
                TenantId = tenant.Id,
                RoleId = tenant.Roles.First().Id
            }
        };

        _tenantRepository.Update(tenant);
        await _tenantRepository.Commit();
        await _searchServiceClient.IndexTenants(new List<Guid>() { tenant.Id });
    }
    
    public async ValueTask<PagedResponse<TenantSummary>> GetTenants(QueryRequest request)
    {
        return _mapper.Map<PagedResponse<TenantSummary>>(await _tenantIndex.FindAll(request));
    }
}

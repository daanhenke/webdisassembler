using AutoMapper;
using WebDisassembler.Core.Application.Models;
using WebDisassembler.Core.Application.Models.Admin;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.Core.ServiceProtocol.Clients;

namespace WebDisassembler.Core.Application.Services.Admin;

public class TenantAdminService
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IMapper _mapper;
    private readonly ISearchServiceClient _searchServiceClient;

    public TenantAdminService(ITenantRepository tenantRepository, IMapper mapper, ISearchServiceClient searchServiceClient)
    {
        _tenantRepository = tenantRepository;
        _mapper = mapper;
        _searchServiceClient = searchServiceClient;
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
}

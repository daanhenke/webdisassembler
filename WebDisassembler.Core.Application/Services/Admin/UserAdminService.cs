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

public class UserAdminService
{
    private readonly IUserRepository _userRepository;
    private readonly ITenantRepository _tenantRepository;
    private readonly IMapper _mapper;
    private readonly ISearchServiceClient _searchServiceClient;
    private readonly IUserIndex _userIndex;

    public UserAdminService(IUserRepository userRepository, IMapper mapper, ISearchServiceClient searchServiceClient, IUserIndex userIndex, ITenantRepository tenantRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _searchServiceClient = searchServiceClient;
        _userIndex = userIndex;
        _tenantRepository = tenantRepository;
    }

    public async ValueTask CreateAdminUser(CreateAdminUser createAdminUser)
    {
        var user = _mapper.Map<User>(createAdminUser);
        user.PasswordHash = "123";
        user.TenantUsers = new List<TenantUser>();
        
        _userRepository.Add(user);
        await _userRepository.Commit();

        var adminTenant = await _tenantRepository.GetAdminTenant();
        
        user.TenantUsers.Add(new TenantUser()
        {
            TenantId = adminTenant.Id,
            RoleId = adminTenant.Roles
                .Where(r => r.IsAdministrator)
                .Select(r => r.Id).First()
        });

        _userRepository.Update(user);
        await _userRepository.Commit();
        await _searchServiceClient.IndexUsers(new List<Guid>() { user.Id });
    }
    
    public async ValueTask<PagedResponse<UserSummary>> GetUsers(QueryRequest request)
    {
        return _mapper.Map<PagedResponse<UserSummary>>(await _userIndex.FindAll(request));
    }
}

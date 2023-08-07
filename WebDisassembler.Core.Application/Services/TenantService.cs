using AutoMapper;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Core.Models.Identity;
using WebDisassembler.Search.Client.Indices;

namespace WebDisassembler.Core.Application.Services;

public class TenantService
{
    private readonly IMapper _mapper;
    private readonly ITenantIndex _tenantIndex;

    public TenantService(IMapper mapper, ITenantIndex tenantIndex)
    {
        _mapper = mapper;
        _tenantIndex = tenantIndex;
    }

    public async ValueTask<PagedResponse<TenantSummary>> GetPublicTenants(PagedRequest request)
    {
        return _mapper.Map<PagedResponse<TenantSummary>>(await _tenantIndex.FindPublic(request));
    }
}
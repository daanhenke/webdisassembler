using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebDisassembler.Core.Application.Models.Identity;
using WebDisassembler.Core.Application.Services;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Core.Identity;

namespace WebDisassembler.Api.Controllers.Admin;

[ApiController, Route("api/tenant")]
public class TenantController : ControllerBase
{
    private readonly TenantService _tenantService;
    private readonly IUserIdentity _userIdentity;

    public TenantController(TenantService tenantService, IUserIdentity userIdentity)
    {
        _tenantService = tenantService;
        _userIdentity = userIdentity;
    }

    [HttpPost("list"), SwaggerOperation(OperationId = nameof(GetPublicTenants))]
    public async ValueTask<PagedResponse<TenantSummary>> GetPublicTenants(PagedRequest request)
    {
        return await _tenantService.GetUserTenants(_userIdentity.UserId, request);
    }
}

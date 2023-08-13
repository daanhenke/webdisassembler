using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebDisassembler.Core.Application.Models;
using WebDisassembler.Core.Application.Models.Admin;
using WebDisassembler.Core.Application.Models.Identity;
using WebDisassembler.Core.Application.Services.Admin;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Core.Identity;

namespace WebDisassembler.Api.Controllers.Admin;

[ApiController, Route("api/admin/tenant")]
public class TenantAdminController : ControllerBase
{
    private readonly TenantAdminService _tenantAdminService;
    private readonly IUserIdentity _userIdentity;

    public TenantAdminController(TenantAdminService tenantAdminService, IUserIdentity userIdentity)
    {
        _tenantAdminService = tenantAdminService;
        _userIdentity = userIdentity;
    }

    [HttpPost("create"), SwaggerOperation(OperationId = nameof(CreateDebugTenant))]
    public async ValueTask<ActionResult> CreateDebugTenant(CreateTenant createTenant)
    {
        await _tenantAdminService.CreateDebugTenant(createTenant, _userIdentity.UserId);
        return Ok();
    }

    [HttpPost("list"), SwaggerOperation(OperationId = nameof(ListTenants))]
    public async ValueTask<PagedResponse<TenantSummary>> ListTenants(QueryRequest request)
    {
        return await _tenantAdminService.GetTenants(request);
    }
}

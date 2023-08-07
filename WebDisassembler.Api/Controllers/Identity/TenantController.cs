using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebDisassembler.Core.Application.Models.Identity;
using WebDisassembler.Core.Application.Services;
using WebDisassembler.Core.Common.Models;

namespace WebDisassembler.Api.Controllers.Admin;

[ApiController, Route("api/tenant")]
public class TenantController : ControllerBase
{
    private readonly TenantService _tenantService;

    public TenantController(TenantService tenantService)
    {
        _tenantService = tenantService;
    }

    [HttpPost("list"), SwaggerOperation(OperationId = nameof(GetPublicTenants))]
    public async ValueTask<PagedResponse<TenantSummary>> GetPublicTenants(PagedRequest request)
    {
        return await _tenantService.GetPublicTenants(request);
    }
}

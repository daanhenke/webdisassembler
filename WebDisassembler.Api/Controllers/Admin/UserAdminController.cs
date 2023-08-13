using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebDisassembler.Core.Application.Models;
using WebDisassembler.Core.Application.Models.Admin;
using WebDisassembler.Core.Application.Models.Identity;
using WebDisassembler.Core.Application.Services.Admin;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Core.Identity;

namespace WebDisassembler.Api.Controllers.Admin;

[ApiController, Route("api/admin/admin")]
public class UserAdminController : ControllerBase
{
    private readonly UserAdminService _userAdminService;
    private readonly IUserIdentity _userIdentity;

    public UserAdminController(UserAdminService userAdminService, IUserIdentity userIdentity)
    {
        _userAdminService = userAdminService;
        _userIdentity = userIdentity;
    }

    [HttpPost("create"), SwaggerOperation(OperationId = nameof(CreateAdminUser))]
    public async ValueTask<ActionResult> CreateAdminUser(CreateAdminUser createAdminUser)
    {
        await _userAdminService.CreateAdminUser(createAdminUser);
        return Ok();
    }

    [HttpPost("list"), SwaggerOperation(OperationId = nameof(ListUsers))]
    public async ValueTask<PagedResponse<UserSummary>> ListUsers(QueryRequest request)
    {
        return await _userAdminService.GetUsers(request);
    }
}

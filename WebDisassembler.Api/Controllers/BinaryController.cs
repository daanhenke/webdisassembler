using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebDisassembler.Core.Application.Models;
using WebDisassembler.Core.Application.Models.Binaries;
using WebDisassembler.Core.Application.Services;
using WebDisassembler.Core.Identity;

namespace WebDisassembler.Api.Controllers;

[ApiController, Route("api/tenants/{tenantId:guid}/projects/{projectId:guid}/binaries")]
public class BinaryController : ControllerBase
{
    private readonly BinaryService _binaryService;
    private readonly IUserIdentity _userIdentity;

    public BinaryController(BinaryService binaryService, IUserIdentity userIdentity)
    {
        _binaryService = binaryService;
        _userIdentity = userIdentity;
    }

    [HttpPost("create"), SwaggerOperation("Create")]
    public async ValueTask<Guid> Create(Guid projectId, CreateBinary createBinary)
    {
        return await _binaryService.CreateBinary(_userIdentity.UserId, projectId, createBinary);
    }

    [HttpGet("{binaryId:guid}/analyze"), SwaggerOperation(OperationId = "Analyze")]
    public async ValueTask<ActionResult> Analyze(Guid projectId, Guid binaryId)
    {
        await _binaryService.StartAnalysis(_userIdentity.UserId, projectId, binaryId);
        return NoContent();
    }
}

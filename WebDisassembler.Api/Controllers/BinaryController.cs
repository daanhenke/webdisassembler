using Microsoft.AspNetCore.Mvc;
using WebDisassembler.Core.Application.Models;
using WebDisassembler.Core.Application.Services;
using WebDisassembler.Core.Identity;

namespace WebDisassembler.Api.Controllers;

[ApiController, Route("api/projects/{projectId:guid}/binaries")]
public class BinaryController : ControllerBase
{
    private readonly BinaryService _binaryService;
    private readonly IdentityDetails _identityDetails;

    public BinaryController(BinaryService binaryService, IdentityDetails identityDetails)
    {
        _binaryService = binaryService;
        _identityDetails = identityDetails;
    }

    [HttpPost("create")]
    public async ValueTask<Guid> Create(Guid projectId, CreateBinary createBinary)
    {
        return await _binaryService.CreateBinary(_identityDetails.UserId!.Value, projectId, createBinary);
    }

    [HttpGet("{binaryId:guid}/analyze")]
    public async ValueTask<ActionResult> Analyze(Guid projectId, Guid binaryId)
    {
        await _binaryService.StartAnalysis(_identityDetails.UserId!.Value, projectId, binaryId);
        return NoContent();
    }
}

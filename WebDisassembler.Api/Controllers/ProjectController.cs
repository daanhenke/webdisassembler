using Microsoft.AspNetCore.Mvc;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Core.Identity;
using WebDisassembler.Core.Models.Projects;
using WebDisassembler.Core.Application.Services;

namespace WebDisassembler.Api.Controllers;

[ApiController, Route("api/tenant/{tenantId:guid}/projects")]
public class ProjectController : ControllerBase
{
    private readonly ILogger<ProjectController> _logger;
    private readonly IUserIdentity _userIdentity;
    private readonly ProjectService _projectService;

    public ProjectController(ILogger<ProjectController> logger, ProjectService projectService, IUserIdentity userIdentity)
    {
        _logger = logger;
        _projectService = projectService;
        _userIdentity = userIdentity;
    }

    [HttpPost("list")]
    public async ValueTask<PagedResponse<ProjectSummary>> List(Guid tenantId, PagedRequest request)
    {
        return await _projectService.GetProjects(_userIdentity.UserId, request);
    }

    [HttpPost("create")]
    public async ValueTask<Guid> Create(Guid tenantId, CreateProject createProject)
    {
        var id = await _projectService.Create(tenantId, _userIdentity.UserId, createProject);
        return id;
    }

    [HttpPost("{projectId:guid}/delete")]
    public async ValueTask<ActionResult> Delete(Guid projectId)
    {
        await _projectService.Delete(projectId);
        return NoContent();
    }
}
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebDisassembler.Core.Application.Models.Projects;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Core.Identity;
using WebDisassembler.Core.Application.Services;

namespace WebDisassembler.Api.Controllers;

[ApiController, Route("api/projects")]
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
    public async ValueTask<Guid> Create(CreateProject createProject)
    {
        var id = await _projectService.Create(_userIdentity.UserId, createProject);
        return id;
    }

    [HttpPost("{projectId:guid}/delete"), SwaggerOperation(OperationId = nameof(Delete))]
    public async ValueTask<ActionResult> Delete(Guid projectId)
    {
        await _projectService.Delete(projectId);
        return NoContent();
    }

    [HttpPost("{projectId:guid}/filetree"), SwaggerOperation(OperationId = nameof(FileTree))]
    public async ValueTask<Dictionary<string, object>> FileTree(Guid projectId)
    {
        return await _projectService.GetProjectFileTree(projectId);
    }
}
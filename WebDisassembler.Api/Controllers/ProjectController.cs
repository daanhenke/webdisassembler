using Microsoft.AspNetCore.Mvc;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Core.Identity;
using WebDisassembler.Core.Models.Projects;
using WebDisassembler.Core.Services;

namespace WebDisassembler.Api.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectController : ControllerBase
{
    private readonly ILogger<ProjectController> _logger;
    private readonly IdentityDetails _identity;
    private readonly ProjectService _projectService;

    public ProjectController(ILogger<ProjectController> logger, ProjectService projectService, IdentityDetails identity)
    {
        _logger = logger;
        _projectService = projectService;
        _identity = identity;
    }

    [HttpPost("list")]
    public async ValueTask<PagedResponse<ProjectSummary>> Create(PagedRequest request)
    {
        return await _projectService.GetProjects(_identity.UserId!.Value, request);
    }

    [HttpPost("create")]
    public async ValueTask<Guid> Create(CreateProject createProject)
    {
        var id = await _projectService.Create(_identity.UserId!.Value, createProject);
        return id;
    }
}
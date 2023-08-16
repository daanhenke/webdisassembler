using AutoMapper;
using WebDisassembler.Core.Application.Models.Projects;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Core.ServiceProtocol.Clients;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Models.Projects;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.Search.Client.Indices;

namespace WebDisassembler.Core.Application.Services;

public class ProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    private readonly ISearchServiceClient _searchServiceClient;
    private readonly IProjectIndex _projectIndex;

    public ProjectService(IProjectRepository projectRepository, IMapper mapper, ISearchServiceClient searchServiceClient, IProjectIndex projectIndex)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
        _searchServiceClient = searchServiceClient;
        _projectIndex = projectIndex;
    }

    public async ValueTask<Guid> Create(Guid userId, CreateProject createProject)
    {
        var project = _mapper.Map<Project>(createProject);
        project.UserId = userId;
        
        _projectRepository.Add(project);
        await _projectRepository.Commit();
        await _searchServiceClient.IndexProjects(new() { project.Id });
        
        return project.Id;
    }

    public async ValueTask Delete(Guid projectId)
    {
        var project = await _projectRepository.GetRequired(projectId, true);

        _projectRepository.Delete(project);
        await _projectRepository.Commit();
    }

    public async ValueTask<PagedResponse<ProjectSummary>> GetUserProjects(Guid userId, PagedRequest request)
    {
        return _mapper.Map<PagedResponse<ProjectSummary>>(await _projectIndex.FindForUser(userId, request));
    }

    public async ValueTask<Dictionary<string, object>> GetProjectFileTree(Guid projectId)
    {
        var project = await _projectIndex.GetById(projectId);
        return project.FileTree;
    }
}
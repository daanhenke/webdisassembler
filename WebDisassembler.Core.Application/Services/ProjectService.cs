﻿using AutoMapper;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Core.Models.Projects;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Models.Projects;
using WebDisassembler.DataStorage.Repositories;

namespace WebDisassembler.Core.Application.Services;

public class ProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async ValueTask<Guid> Create(Guid tenantId, Guid userId, CreateProject createProject)
    {
        var project = _mapper.Map<Project>(createProject);
        project.UserId = userId;
        project.TenantId = tenantId;
        
        _projectRepository.Add(project);
        await _projectRepository.Commit();

        return project.Id;
    }

    public async ValueTask Delete(Guid projectId)
    {
        var project = await _projectRepository.GetRequired(projectId, true);

        _projectRepository.Delete(project);
        await _projectRepository.Commit();
    }

    public async ValueTask<PagedResponse<ProjectSummary>> GetProjects(Guid userId, PagedRequest request)
    {
        throw new NotImplementedException();
        // var projects = await _projectRepository.GetProjects(userId, request);
        // return _mapper.Map<PagedResponse<Project>, PagedResponse<ProjectSummary>>(projects);
    }
}
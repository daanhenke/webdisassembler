using AutoMapper;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Core.Models.Projects;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Repositories;

namespace WebDisassembler.Core.Services;

public class ProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async ValueTask<Guid> Create(Guid userId, CreateProject createProject)
    {
        var project = new Project
        {
            Name = createProject.Name,
            OwnerId = userId
        };

        _projectRepository.Add(project);
        await _projectRepository.Commit();

        var binary = new Binary
        {
            Name = "Test Binary",
            FilePath = "test",
            OwnerId = userId
        };

        for (var i = 0; i < 3; i++)
        {
            binary.Sections.Add(new Section
            {
                Name = $"Section {i + 1}"
            });
        }
        
        project.Binaries.Add(binary);

        _projectRepository.Update(project);
        await _projectRepository.Commit();

        return project.Id;
    }

    public async ValueTask<PagedResponse<ProjectSummary>> GetProjects(Guid userId, PagedRequest request)
    {
        var projects = await _projectRepository.GetProjects(userId, request);
        return _mapper.Map<PagedResponse<Project>, PagedResponse<ProjectSummary>>(projects);
    }
}
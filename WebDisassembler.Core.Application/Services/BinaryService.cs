using AutoMapper;
using WebDisassembler.Core.Application.Models;
using WebDisassembler.Core.Application.Models.Binaries;
using WebDisassembler.DataStorage.Models.Projects;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.FileStorage;
using WebDisassembler.Core.ServiceProtocol.Clients;

namespace WebDisassembler.Core.Application.Services;

public class BinaryService
{
    private readonly IFileStorage _fileStorage;
    private readonly IProjectRepository _projectRepository;
    private readonly IAnalysisServiceClient _analysisServiceClient;
    private readonly ISearchServiceClient _searchServiceClient;
    private readonly IMapper _mapper;

    public BinaryService(IFileStorage fileStorage, IProjectRepository projectRepository, IAnalysisServiceClient analysisServiceClient, IMapper mapper, ISearchServiceClient searchServiceClient)
    {
        _fileStorage = fileStorage;
        _projectRepository = projectRepository;
        _analysisServiceClient = analysisServiceClient;
        _mapper = mapper;
        _searchServiceClient = searchServiceClient;
    }

    public async ValueTask<Guid> CreateBinary(Guid userId, Guid projectId, CreateBinary createBinary)
    {
        var project = await _projectRepository.GetWithBinaries(projectId, true);
        
        await _fileStorage.MoveTemporaryFile(project.TenantId, userId, createBinary.FileId, $"projects/{projectId}/binaries");

        var binary = _mapper.Map<Binary>(createBinary);
        
        project.Binaries.Add(binary);
        _projectRepository.Update(project);
        await _projectRepository.Commit();

        await _searchServiceClient.IndexProjects(new() { projectId });
        
        return binary.Id;
    }

    public async ValueTask StartAnalysis(Guid userId, Guid projectId, Guid binaryId)
    {
        await _analysisServiceClient.StartBinaryAnalysis(projectId, binaryId);
    }
}

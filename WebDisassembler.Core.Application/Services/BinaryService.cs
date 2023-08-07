using WebDisassembler.Core.Application.Models;
using WebDisassembler.DataStorage.Models.Projects;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.FileStorage;
using WebDisassembler.ServiceProtocol.Clients;

namespace WebDisassembler.Core.Application.Services;

public class BinaryService
{
    private readonly IFileStorage _fileStorage;
    private readonly IProjectRepository _projectRepository;
    private readonly IAnalysisServiceClient _analysisServiceClient;

    public BinaryService(IFileStorage fileStorage, IProjectRepository projectRepository, IAnalysisServiceClient analysisServiceClient)
    {
        _fileStorage = fileStorage;
        _projectRepository = projectRepository;
        _analysisServiceClient = analysisServiceClient;
    }

    public async ValueTask<Guid> CreateBinary(Guid userId, Guid projectId, CreateBinary createBinary)
    {
        var project = await _projectRepository.GetWithBinaries(projectId, true);
        
        await _fileStorage.MoveTemporaryFile(project.TenantId, userId, createBinary.TemporaryFileId, $"projects/{projectId}/binaries");
        
        var binary = new Binary
        {
            ProjectId = project.TenantId,
            Filename = createBinary.Name,
            FileId = createBinary.TemporaryFileId
        };
        
        project.Binaries.Add(binary);
        _projectRepository.Update(project);
        await _projectRepository.Commit();
        
        return binary.Id;
    }

    public async ValueTask StartAnalysis(Guid userId, Guid projectId, Guid binaryId)
    {
        await _analysisServiceClient.StartAnalyzeBinary(binaryId);
    }
}

using WebDisassembler.Core.Application.Models;
using WebDisassembler.DataStorage.Models;
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
        throw new NotImplementedException();
        // var project = await _projectRepository.GetRequired(projectId, true);
        //
        // await _fileStorage.MoveTemporaryFile(userId, createBinary.TemporaryFileId, $"projects/{projectId}/binaries");
        //
        // var binary = new Binary
        // {
        //     OwnerId = userId,
        //     Name = createBinary.Name,
        //     FilePath = "test"
        // };
        //
        // project.Binaries.Add(binary);
        // await _projectRepository.Commit();
        //
        // return binary.Id;
    }

    public async ValueTask StartAnalysis(Guid userId, Guid projectId, Guid binaryId)
    {
        await _analysisServiceClient.StartAnalyzeBinary(binaryId);
    }
}

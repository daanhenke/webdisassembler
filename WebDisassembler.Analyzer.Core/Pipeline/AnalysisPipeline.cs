using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.FileStorage;

namespace WebDisassembler.Analyzer.Core.Pipeline;

public class AnalysisPipeline
{
    private readonly IProjectRepository _projectRepository;
    private readonly IFileStorage _fileStorage;

    public AnalysisPipeline(IProjectRepository projectRepository, IFileStorage fileStorage)
    {
        _projectRepository = projectRepository;
        _fileStorage = fileStorage;
    }

    public async ValueTask StartAnalysis(Guid projectId, Guid binaryId)
    {
        var project = await _projectRepository.GetWithBinaries(projectId, true);
    }
}
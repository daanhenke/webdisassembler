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
        var binary = project.Binaries.FirstOrDefault(b => b.Id == binaryId);
        if (binary == null)
        {
            throw new NotSupportedException();
        }

        var (stream, reference) = await _fileStorage.OpenRead(binary.FileId);
        using var reader = new BinaryReader(stream);
        var test = reader.ReadUInt16().ToString("X");
    }
}
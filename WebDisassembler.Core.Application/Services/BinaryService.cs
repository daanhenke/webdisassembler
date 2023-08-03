using WebDisassembler.Core.Application.Models;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.FileStorage;

namespace WebDisassembler.Core.Application.Services;

public class BinaryService
{
    private readonly IFileStorage _fileStorage;
    private readonly IProjectRepository _projectRepository;

    public BinaryService(IFileStorage fileStorage, IProjectRepository projectRepository)
    {
        _fileStorage = fileStorage;
        _projectRepository = projectRepository;
    }

    public async ValueTask<Guid> CreateBinary(Guid userId, Guid projectId, CreateBinary createBinary)
    {
        var project = await _projectRepository.GetRequired(projectId, true);

        var binary = new Binary
        {
            OwnerId = userId,
            Name = createBinary.Name,
            FilePath = "test"
        };

        project.Binaries.Add(binary);

        return binary.Id;
    }
}

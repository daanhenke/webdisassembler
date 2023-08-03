using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.FileStorage;

namespace WebDisassembler.Core.Application.Services;

public class FileService
{
    private readonly IFileStorage _fileStorage;

    public FileService(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }

    public async ValueTask<Guid> UploadTemporaryFile(Guid ownerId, Stream stream, string fileName)
    {
        return await _fileStorage.UploadTemporaryFile(ownerId, stream, fileName);
    }
}

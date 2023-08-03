using Azure.Storage.Blobs;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Repositories;

namespace WebDisassembler.FileStorage.Impl;

public class BlobStorageFileStorage : IFileStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IFileReferenceRepository _fileReferenceRepository;

    public BlobStorageFileStorage(IFileReferenceRepository fileReferenceRepository)
    {
        _blobServiceClient = new("DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");
        _fileReferenceRepository = fileReferenceRepository;
    }

    public async ValueTask<Guid> UploadTemporaryFile(Guid userId, Stream stream, string fileName)
    {
        var reference = new FileReference()
        {
            IsTemporary = true,
            FileName = fileName,
            OwnerId = userId,
            Path = ""
        };

        _fileReferenceRepository.Add(reference);
        await _fileReferenceRepository.Commit();

        var temporaryPath = GenerateTemporaryPath(reference);
        reference.Path = temporaryPath;
        _fileReferenceRepository.Update(reference);
        await _fileReferenceRepository.Commit();

        var blob = await GetBlob(temporaryPath);
        await blob.UploadAsync(stream, true);

        return reference.Id;
    }

    public async ValueTask DeleteTemporaryFile(Guid userId, Guid fileId)
    {
        var reference = await _fileReferenceRepository.GetRequired(fileId, true);
        _fileReferenceRepository.Delete(reference);

        var blob = await GetBlob(GenerateTemporaryPath(reference));
        await blob.DeleteAsync();
    }

    public ValueTask MoveTemporaryFile(Guid userId, Guid temporaryFileId, string path)
    {
        throw new NotImplementedException();
    }

    public ValueTask Delete(string path)
    {
        throw new NotImplementedException();
    }

    private async ValueTask<BlobClient> GetBlob(string path)
    {
        var container = await GetContainerClient("persistent");
        return container.GetBlobClient(path);
        
    }

    private async ValueTask<BlobClient> GetTemporaryBlob(string path)
    {
        var container = await GetContainerClient("temporary");
        return container.GetBlobClient(path);
    }

    private async ValueTask<BlobContainerClient> GetContainerClient(string container)
    {
        var existingContainer = _blobServiceClient.GetBlobContainerClient(container);

        if (existingContainer != null)
        {
            return existingContainer;
        }

        var createResult = await _blobServiceClient.CreateBlobContainerAsync(container);
        return createResult.Value;
    }

    private string GenerateTemporaryPath(FileReference reference)
    {
        return $"{reference.OwnerId}/{reference.Id}-{reference.FileName}";
    }
}
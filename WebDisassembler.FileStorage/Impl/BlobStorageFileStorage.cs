using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.FileStorage.Options;

namespace WebDisassembler.FileStorage.Impl;

public class BlobStorageFileStorage : IFileStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IFileReferenceRepository _fileReferenceRepository;
    private readonly IOptions<FileStorageOptions> _options;

    public BlobStorageFileStorage(IFileReferenceRepository fileReferenceRepository, IOptions<FileStorageOptions> options)
    {
        _fileReferenceRepository = fileReferenceRepository;
        _options = options;
        _blobServiceClient = new(_options.Value.ConnectionString);
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

        var blob = await GetTemporaryBlob(temporaryPath);
        await blob.UploadAsync(stream, true);

        return reference.Id;
    }

    public async ValueTask DeleteTemporaryFile(Guid userId, Guid fileId)
    {
        var reference = await _fileReferenceRepository.GetRequired(fileId, true);
        _fileReferenceRepository.Delete(reference);

        var blob = await GetTemporaryBlob(GenerateTemporaryPath(reference));
        await blob.DeleteAsync();
    }

    public async ValueTask<FileReference> MoveTemporaryFile(Guid userId, Guid temporaryFileId, string path)
    {
        var reference = await _fileReferenceRepository.GetRequired(temporaryFileId, true);

        var tempBlob = await GetTemporaryBlob(GenerateTemporaryPath(reference));

        var persistentPath = GeneratePersistentPath(reference, path);
        var persistentBlob = await GetBlob(persistentPath);

        var tempUri = tempBlob.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.Now.AddMinutes(20));
        var copyOperation = await persistentBlob.StartCopyFromUriAsync(tempUri);

        await copyOperation.WaitForCompletionAsync();
        await tempBlob.DeleteAsync();

        reference.IsTemporary = false;
        reference.Path = persistentPath;
        _fileReferenceRepository.Update(reference);

        await _fileReferenceRepository.Commit();

        return reference;
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

    private async ValueTask<BlobContainerClient> GetContainerClient(string containerName)
    {
        var container = _blobServiceClient.GetBlobContainerClient(containerName);
        await container.CreateIfNotExistsAsync();

        return container;
    }

    private string GenerateTemporaryPath(FileReference reference)
    {
        return $"{reference.OwnerId}/{reference.Id}-{reference.FileName}";
    }

    private string GeneratePersistentPath(FileReference reference, string directoryPath)
    {
        return $"{directoryPath}/{reference.Id}-{reference.FileName}";
    }
}
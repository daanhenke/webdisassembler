﻿using WebDisassembler.DataStorage.Models;

namespace WebDisassembler.FileStorage;

public interface IFileStorage
{
    public ValueTask<Guid> UploadTemporaryFile(Guid userId, Stream stream, string fileName);
    public ValueTask DeleteTemporaryFile(Guid userId, Guid fileId);
    public ValueTask<FileReference> MoveTemporaryFile(Guid tenantId, Guid userId, Guid temporaryFileId, string path);

    public ValueTask Delete(string path);
}

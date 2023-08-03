using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Repositories.Impl;

public class FileReferenceRepository : RepositoryBase<FileReference>, IFileReferenceRepository
{
    public FileReferenceRepository(DatabaseContext database) : base(database, () => database.FileReferences) {}
}

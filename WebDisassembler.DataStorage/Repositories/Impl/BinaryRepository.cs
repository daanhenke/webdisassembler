using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Repositories.Impl;

public class BinaryRepository : RepositoryBase<Binary>, IBinaryRepository
{
    public BinaryRepository(DatabaseContext database) : base(database, () => database.Binaries) {}
}

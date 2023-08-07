using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Repositories.Impl;

public class BinaryRepository : RepositoryBase<User>, IBinaryRepository
{
    public BinaryRepository(DatabaseContext database) : base(database, () => database.Users) {}
}

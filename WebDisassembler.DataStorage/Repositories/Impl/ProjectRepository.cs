using Microsoft.EntityFrameworkCore;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Repositories.Impl;

public class ProjectRepository : RepositoryBase<User>, IProjectRepository
{
    public ProjectRepository(DatabaseContext database) : base(database, () => database.Users)
    { }

    public async ValueTask<PagedResponse<User>> GetProjects(Guid userId, PagedRequest request)
    {
        return await Query().ToPaged(request);
    }
}
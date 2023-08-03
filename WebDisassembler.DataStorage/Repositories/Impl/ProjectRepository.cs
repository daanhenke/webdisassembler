using Microsoft.EntityFrameworkCore;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Repositories.Impl;

public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
{
    public ProjectRepository(DatabaseContext database) : base(database, () => database.Projects)
    { }

    public async ValueTask<PagedResponse<Project>> GetProjects(Guid userId, PagedRequest request)
    {
        return await Query()
            .Include(p => p.Binaries)
            .Where(p => p.OwnerId == userId)
            .ToPaged(request);
    }
}
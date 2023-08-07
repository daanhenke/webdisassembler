using Microsoft.EntityFrameworkCore;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Models.Projects;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Repositories.Impl;

public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
{
    public ProjectRepository(DatabaseContext database) : base(database, () => database.Projects)
    { }

    public async ValueTask<PagedResponse<Project>> GetAllForIndex(PagedRequest request)
    {
        return await Query()
            .Include(p => p.ProjectMembers)
            .Include(p => p.Binaries)
            .ToPaged(request);
    }

    public async ValueTask<Project> GetWithBinaries(Guid projectId, bool tracked)
    {
        return await QueryRequired(projectId, tracked, q => q
            .Include(p => p.Binaries)
            .FirstOrDefaultAsync(p => p.Id == projectId)
        );
    }
}
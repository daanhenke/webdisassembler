using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Models.Projects;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Repositories;

public interface IProjectRepository : IRepository<Project>
{
    ValueTask<PagedResponse<Project>> GetAllForIndex(PagedRequest request);
    ValueTask<Project> GetWithBinaries(Guid projectId, bool tracked);
    ValueTask<IReadOnlyCollection<Project>> GetForIndex(ISet<Guid> ids, bool tracked);
}
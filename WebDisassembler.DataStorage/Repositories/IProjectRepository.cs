using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Repositories;

public interface IProjectRepository : IRepository<Project>
{
    ValueTask<PagedResponse<Project>> GetProjects(Guid userId, PagedRequest request);
}
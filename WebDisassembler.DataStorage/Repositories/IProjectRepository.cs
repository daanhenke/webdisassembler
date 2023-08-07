using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Repositories;

public interface IProjectRepository : IRepository<User>
{
    ValueTask<PagedResponse<User>> GetProjects(Guid userId, PagedRequest request);
}
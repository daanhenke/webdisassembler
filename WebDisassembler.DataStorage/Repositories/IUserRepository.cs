using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Repositories;

public interface IUserRepository : IRepository<User>
{
    ValueTask<User?> GetUserForLogin(string emailOrUsername, bool tracked);
    ValueTask<User?> GetUserByToken(string token, DateTimeOffset expirationDate, bool tracked);
    ValueTask<User> GetUserForCurrentUser(Guid userId, bool tracked);
    ValueTask<PagedResponse<User>> GetAllForIndex(PagedRequest request);
}
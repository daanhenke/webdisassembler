using Microsoft.EntityFrameworkCore;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Repositories.Impl;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(DatabaseContext database) : base(database, () => database.Users) { }

    public async ValueTask<User?> GetUserForLogin(string emailOrUsername, bool tracked)
    {
        return await Query(tracked)
            .Include(u => u.AuthenticationTokens)
            .FirstOrDefaultAsync(u => u.Email == emailOrUsername || u.Username == emailOrUsername);
    }
    
    public async ValueTask<User?> GetUserByToken(string token, DateTimeOffset expirationDate, bool tracked)
    {
        return await Query(tracked)
            .Include(u => u.AuthenticationTokens)
            .FirstOrDefaultAsync(u => u.AuthenticationTokens
                .Any(t =>
                    t.Token == token &&
                    t.ExpiresBy >= expirationDate
                )
            );
    }

    public async ValueTask<User> GetUserForCurrentUser(Guid userId, bool tracked)
    {
        return await QueryRequired(userId, tracked, q => q
            .Include(u => u.Tenants)
            .FirstOrDefaultAsync(u => u.Id == userId)
        );
    }

    public async ValueTask<PagedResponse<User>> GetAllForIndex(PagedRequest request)
    {
        return await Query()
            .Include(u => u.Tenants)
            .ToPaged(request);
    }
}
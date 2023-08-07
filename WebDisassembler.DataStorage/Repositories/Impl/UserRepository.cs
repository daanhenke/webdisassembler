using Microsoft.EntityFrameworkCore;
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
}
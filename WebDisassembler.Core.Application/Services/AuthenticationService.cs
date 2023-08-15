using System.Text;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WebDisassembler.Core.Application.Exceptions.Authentication;
using WebDisassembler.Core.Application.Models.Authentication;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.Search.Client.Indices;

namespace WebDisassembler.Core.Application.Services;

public class AuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserIndex _userIndex;
    private readonly IMapper _mapper;
    private readonly ILogger<Microsoft.AspNetCore.Authentication.AuthenticationService> _logger;

    public AuthenticationService(IUserRepository userRepository, IMapper mapper, IUserIndex userIndex, ILogger<Microsoft.AspNetCore.Authentication.AuthenticationService> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userIndex = userIndex;
        _logger = logger;
    }

    public async ValueTask<string> Login(LoginRequest request)
    {
        var user = await _userRepository.GetUserForLogin(request.UsernameOrEmail, false);
        if (user == null || user.PasswordHash != HashPassword(request.Password))
        {
            throw new LoginFailedException();
        }

        var token = new AuthenticationToken()
        {
            ExpiresBy = GenerateExpirationDate(),
            Token = GenerateToken()
        };

        user.AuthenticationTokens.Add(token);
        _userRepository.Update(user);
        await _userRepository.Commit();

        _logger.LogInformation("User {UserId} succesfully logged in", user.Id);
        
        return token.Token;
    }    
    
    public async ValueTask<CurrentUser> GetCurrentUser(Guid userId)
    {
        var user = await _userIndex.GetById(userId);
        return _mapper.Map<CurrentUser>(user);
    }

    public async ValueTask<User?> GetUserFromToken(string token)
    {
        var user = await _userRepository.GetUserByToken(token, DateTimeOffset.UtcNow, true);
        if (user == null)
        {
            return null;
        }

        var usedToken = user.AuthenticationTokens.First(t => t.Token == token);
        usedToken.ExpiresBy = GenerateExpirationDate();
        
        _userRepository.Update(user);
        await _userRepository.Commit();
        return user;
    }

    private DateTimeOffset GenerateExpirationDate()
    {
        return DateTimeOffset.UtcNow.AddDays(7);
    }

    private string GenerateToken()
    {
        var builder = new StringBuilder();
        var random = new Random();

        const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
        for (var i = 0; i < 128; i++)
        {
            var index = random.Next() % 64;
            builder.Append(allowedChars[index]);
        }

        return builder.ToString();
    }

    private string HashPassword(string password)
    {
        return password;
    }
}
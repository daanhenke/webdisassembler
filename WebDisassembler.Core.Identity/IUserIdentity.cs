namespace WebDisassembler.Core.Identity;

public interface IUserIdentity
{
    public bool IsLoggedIn { get; }
    public Guid UserId { get; }
}
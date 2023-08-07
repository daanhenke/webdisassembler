namespace WebDisassembler.Core.Application.Exceptions.Authentication;

public class LoginFailedException : Exception
{
    public LoginFailedException() : base("login_failed") { }
}
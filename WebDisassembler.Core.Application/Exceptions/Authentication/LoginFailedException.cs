namespace WebDisassembler.Core.Exceptions.Authentication;

public class LoginFailedException : Exception
{
    public LoginFailedException() : base("login_failed") { }
}
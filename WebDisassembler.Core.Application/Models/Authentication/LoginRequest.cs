namespace WebDisassembler.Core.Models.Authentication;

public class LoginRequest
{
    public required string UsernameOrEmail { get; set; }
    public required string Password { get; set; }
}
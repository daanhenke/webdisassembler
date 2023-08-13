namespace WebDisassembler.Core.Application.Models.Identity;

public class UserSummary
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
}
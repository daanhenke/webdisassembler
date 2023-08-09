using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models.Identity;

[PrimaryKey(nameof(Token), nameof(UserId))]
public class AuthenticationToken
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public required string Token { get; set; }
    public required DateTimeOffset ExpiresBy { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models.Identity;

[PrimaryKey(nameof(Id))]
public class User : IIdentifiableEntity
{
    public Guid Id { get; set; }

    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }

    [ForeignKey(nameof(AuthenticationToken.UserId))]
    public ICollection<AuthenticationToken> AuthenticationTokens { get; set; }

}
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models;

[PrimaryKey(nameof(Id))]
public class FileReference : IIdentifiableEntity, IOwnedEntity
{
    public Guid Id { get; set; }
    public Guid? TenantId { get; set; }
    public Tenant? Tenant { get;  set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public required bool IsTemporary { get; set; }
    public required string Path { get; set; }
    public required string FileName { get; set; }
}

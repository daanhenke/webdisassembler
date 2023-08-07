using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models;

[PrimaryKey(nameof(Id))]
public class FileReference : IIdentifiableEntity, ITenantEntity, IOwnedEntity, IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid UserId { get; set; }

    public required bool IsTemporary { get; set; }
    public required string Path { get; set; }
    public required string FileName { get; set; }
    
    public Guid CreatedBy { get; set; }
    public DateTimeKind CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}

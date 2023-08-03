using System.ComponentModel.DataAnnotations;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models;

public class FileReference : IIdentifiableEntity, IOwnedEntity, IAuditableEntity
{
    [Key] public Guid Id { get; set; }
    public Guid OwnerId { get; set; }

    public required bool IsTemporary { get; set; }
    public required string Path { get; set; }
    public required string FileName { get; set; }
    
    public Guid CreatedBy { get; set; }
    public DateTimeKind CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models;

[PrimaryKey(nameof(OwnerId), nameof(ProjectId), nameof(BinaryId), nameof(Id))]
public class Section : IIdentifiableEntity, IOwnedEntity, IAuditableEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid BinaryId { get; set; }
    
    public required string Name { get; set; }
    
    public Guid CreatedBy { get; set; }
    public DateTimeKind CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
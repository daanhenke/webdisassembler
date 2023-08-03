
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models;

[PrimaryKey(nameof(ProjectId), nameof(Id))]
public class Binary : IIdentifiableEntity, IOwnedEntity, IAuditableEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid OwnerId { get; set; }
    
    public required string Name { get; set; }
    public required string FilePath { get; set; }

    public List<Section> Sections = new();
    
    public Guid CreatedBy { get; set; }
    public DateTimeKind CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
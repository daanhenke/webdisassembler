using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models;

[PrimaryKey(nameof(Id))]
public class Project : IIdentifiableEntity, IOwnedEntity, IAuditableEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    
    public required string Name { get; set; }
    public List<Binary> Binaries { get; init; } = new();
    
    public Guid CreatedBy { get; set; }
    public DateTimeKind CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
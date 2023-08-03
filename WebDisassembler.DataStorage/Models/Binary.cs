
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models;

[PrimaryKey(nameof(OwnerId), nameof(ProjectId), nameof(Id))]
public class Binary : IOwnedEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid OwnerId { get; set; }
    
    public required string Name { get; set; }
    public required string FilePath { get; set; }

    public List<Section> Sections = new();
}
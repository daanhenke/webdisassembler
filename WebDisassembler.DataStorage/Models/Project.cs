using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models;

[PrimaryKey(nameof(OwnerId), nameof(Id))]
public class Project : IOwnedEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    
    public required string Name { get; set; }
    public List<Binary> Binaries { get; init; } = new();
}
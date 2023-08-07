using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models.Identity;

[PrimaryKey(nameof(Id))]
public class Tenant : IIdentifiableEntity, IOwnedEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public required string Subdomain { get; set; }
}
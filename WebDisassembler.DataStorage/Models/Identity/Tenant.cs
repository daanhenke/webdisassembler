using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Models.Projects;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models.Identity;

[PrimaryKey(nameof(Id))]
public class Tenant : IIdentifiableEntity, IOwnedEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public required string Subdomain { get; set; }
    public required bool Public { get; set; }

    public ICollection<FileReference> FileReferences { get; set; } = null!;

    public ICollection<User> Users { get; set; } = null!;
    public ICollection<TenantUser> TenantUsers { get; set; } = null!;

    public ICollection<Role> Roles { get; set; } = null!;
    
    public ICollection<Project> Projects { get; set; } = null!;
}
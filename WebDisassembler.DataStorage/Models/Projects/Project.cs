using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models.Projects;

public class Project : IIdentifiableEntity, ITenantEntity, IOwnedEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public required string Name { get; set; }
    public required string ShortDescription { get; set; }
    
    public ICollection<ProjectMember> ProjectMembers { get; set; } = null!;
    public ICollection<User> Users { get; set; } = null!;
    public ICollection<Binary> Binaries { get; set; } = null!;
}
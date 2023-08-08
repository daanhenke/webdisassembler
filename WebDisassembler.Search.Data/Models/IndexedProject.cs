using WebDisassembler.Search.Data.Utility;

namespace WebDisassemlber.Search.Data.Models;

public class IndexedProject : IIndexedEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public required string ShortDescription { get; set; }
    public List<IndexedProjectMember> ProjectMembers { get; set; } = null!;
    public List<string> BinaryNames { get; set; } = null!;
    public Dictionary<string, object> FileTree { get; set; }  = null!;
}

public class IndexedProjectMember
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}

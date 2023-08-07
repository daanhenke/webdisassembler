namespace WebDisassemlber.Search.Data.Models;

public class IndexedProject
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string ShortDescription { get; set; }
    public Guid UserId { get; set; }
    public List<IndexedProjectMember> ProjectMembers { get; set; }
    public List<string> BinaryNames { get; set; }
}

public class IndexedProjectMember
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
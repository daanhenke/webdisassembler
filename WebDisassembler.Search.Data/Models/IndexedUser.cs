using WebDisassembler.Search.Data.Utility;

namespace WebDisassemlber.Search.Data.Models;

public class IndexedUser : IIndexedEntity
{
    public Guid Id { get; set; }
    public bool IsAdministrator { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required List<IndexedUserTenant> Tenants { get; set; }
}

public class IndexedUserTenant
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}
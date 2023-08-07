using WebDisassembler.Search.Data.Utility;

namespace WebDisassemlber.Search.Data.Models;

public class IndexedUser : IIndexedEntity
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
}

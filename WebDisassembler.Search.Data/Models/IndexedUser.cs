namespace WebDisassemlber.Search.Data.Models;

public class IndexedUser
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
}

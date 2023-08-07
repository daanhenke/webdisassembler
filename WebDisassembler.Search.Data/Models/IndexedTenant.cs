namespace WebDisassemlber.Search.Data.Models;

public class IndexedTenant
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Subdomain { get; set; }
    public bool Public { get; set; }
}

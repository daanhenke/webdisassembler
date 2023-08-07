namespace WebDisassembler.Core.Models.Identity;

public class TenantSummary
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Subdomain { get; set; }
}
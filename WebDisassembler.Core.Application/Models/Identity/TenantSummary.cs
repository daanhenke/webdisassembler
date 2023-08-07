namespace WebDisassembler.Core.Application.Models.Identity;

public class TenantSummary
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Subdomain { get; set; }
}
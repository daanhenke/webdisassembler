namespace WebDisassembler.Core.Application.Models.Projects;

public class ProjectSummary
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string ShortDescription { get; set; }
    
    public List<BinarySummary> Binaries { get; set; } = null!;
}
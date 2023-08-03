namespace WebDisassembler.Core.Models.Projects;

public class ProjectSummary
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public List<BinarySummary> Binaries { get; set; }
}
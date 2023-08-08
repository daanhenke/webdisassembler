using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models.Projects;

public class Binary : IIdentifiableEntity
{
    public Guid Id { get; set; }
    
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    
    public required string ProjectPath { get; set; }
    
    public Guid FileId { get; set; }
    public FileReference File { get; set; } = null!;
    
    public ICollection<BinarySection> Sections { get; set; } = null!;
    
    public Dictionary<string, string> Metadata { get; set; } = null!;
    public BinaryAnalysisStatus AnalysisStatus { get; set; }
}
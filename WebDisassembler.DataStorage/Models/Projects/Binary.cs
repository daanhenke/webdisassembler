using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models.Projects;

public class Binary : IIdentifiableEntity
{
    public Guid Id { get; set; }
    
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
    
    public required string Filename { get; set; }
    
    public Guid FileId { get; set; }
    public FileReference File { get; set; }
    
    public ICollection<BinarySection> Sections { get; set; }
}
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models.Projects;

public class BinarySection : IIdentifiableEntity
{
    public Guid Id { get; set; }
    
    public Guid BinaryId { get; set; }
    public Binary Binary { get; set; } = null!;
    
    public required string Name { get; set; }
    
    public ulong Begin { get; set; }
    public ulong End { get; set; }
    
    public bool CanRead { get; set; }
    public bool CanWrite { get; set; }
    public bool CanExecute { get; set; }
}
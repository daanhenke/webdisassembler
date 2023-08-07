using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models.Projects;

public class BinarySection : IIdentifiableEntity
{
    public Guid Id { get; set; }
    
    public Guid BinaryId { get; set; }
    public Binary Binary { get; set; }
    
    public string Name { get; set; }
}
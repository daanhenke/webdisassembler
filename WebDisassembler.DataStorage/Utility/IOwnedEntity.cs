namespace WebDisassembler.DataStorage.Utility;

public interface IOwnedEntity
{
    public Guid OwnerId { get; set; }
}
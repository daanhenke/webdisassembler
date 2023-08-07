namespace WebDisassembler.DataStorage.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(Guid id) : base($"Model with id {id} not found") {} 
}

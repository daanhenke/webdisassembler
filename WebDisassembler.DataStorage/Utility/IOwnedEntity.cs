namespace WebDisassembler.DataStorage.Utility;

public interface IOwnedEntity
{
    public Guid UserId { get; set; }
}

public interface IAuditableEntity
{
    public Guid CreatedBy { get; set; }
    public DateTimeKind CreatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
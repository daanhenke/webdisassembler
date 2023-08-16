namespace WebDisassembler.Core.Application.Models.CommandPalette;

public class GotoTenantCommand : AbstractCommandPaletteCommand
{
    public override CommandType Type => CommandType.GotoTenant;
    public Guid TenantId { get; set; }
}
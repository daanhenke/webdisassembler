namespace WebDisassembler.Core.Application.Models.CommandPalette;

public class GotoProjectCommand : AbstractCommandPaletteCommand
{
    public override CommandType Type => CommandType.GotoProject;
    public Guid ProjectId { get; set; }
}
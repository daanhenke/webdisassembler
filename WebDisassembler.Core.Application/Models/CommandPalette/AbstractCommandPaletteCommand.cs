using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace WebDisassembler.Core.Application.Models.CommandPalette;

[SwaggerDiscriminator("type")]
[SwaggerSubType(typeof(GotoProjectCommand), DiscriminatorValue = nameof(CommandType.GotoProject))]
[JsonDerivedType(typeof(GotoProjectCommand))]
[SwaggerSubType(typeof(GotoTenantCommand), DiscriminatorValue = nameof(CommandType.GotoTenant))]
[JsonDerivedType(typeof(GotoTenantCommand))]
public abstract class AbstractCommandPaletteCommand
{
    public abstract CommandType Type { get; }
}
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebDisassembler.Core.Application.Models.CommandPalette;

namespace WebDisassembler.Api.Controllers;

[ApiController, Route("api/command-palette")]
public class CommandPaletteController : ControllerBase
{
    [HttpGet("query"), SwaggerOperation(OperationId = nameof(QueryCommands))]
    public List<AbstractCommandPaletteCommand> QueryCommands(string? query)
    {
        return new List<AbstractCommandPaletteCommand>()
        {
            new GotoProjectCommand
            {
                ProjectId = Guid.NewGuid()
            },
            new GotoTenantCommand()
            {
                TenantId = Guid.NewGuid()
            }
        };
    }
}
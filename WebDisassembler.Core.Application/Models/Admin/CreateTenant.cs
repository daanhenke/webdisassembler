using System.ComponentModel.DataAnnotations;

namespace WebDisassembler.Core.Application.Models;

public class CreateTenant
{
    [Required] public required string Name { get; set; }
    [Required] public required string Subdomain { get; set; }
    [Required] public bool Public { get; set; }
}

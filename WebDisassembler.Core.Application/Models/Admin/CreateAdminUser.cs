using System.ComponentModel.DataAnnotations;

namespace WebDisassembler.Core.Application.Models.Admin;

public class CreateAdminUser
{
    [Required] public required string Username { get; set; }
    [Required] public required string Email { get; set; }
}

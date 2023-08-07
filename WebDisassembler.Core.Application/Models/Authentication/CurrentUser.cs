using System.ComponentModel.DataAnnotations;

namespace WebDisassembler.Core.Models.Authentication;

public class CurrentUser
{
    [Required] public Guid UserId { get; set; }
    [Required] public required string Username { get; set; }
}
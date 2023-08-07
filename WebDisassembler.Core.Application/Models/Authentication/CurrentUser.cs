using System.ComponentModel.DataAnnotations;

namespace WebDisassembler.Core.Models.Authentication;

public class CurrentUser
{
    [Required] public Guid UserId { get; set; }
    [Required] public required string Username { get; set; }
    [Required] public required List<CurrentUserTenant> Tenants { get; set; }
}

public class CurrentUserTenant
{
    [Required] public Guid TenantId { get; set; }
    [Required] public required string Name { get; set; }
}
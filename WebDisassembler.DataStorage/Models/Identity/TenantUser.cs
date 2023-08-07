using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models.Identity;

[PrimaryKey(nameof(UserId), nameof(TenantId))]
public class TenantUser : ITenantEntity, IOwnedEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; }
    public Guid RoleId { get; set; }
}

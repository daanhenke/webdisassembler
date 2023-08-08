using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebDisassembler.DataStorage.Models.Projects;
using WebDisassembler.DataStorage.Utility;

namespace WebDisassembler.DataStorage.Models.Identity;

[PrimaryKey(nameof(Id))]
public class User : IIdentifiableEntity
{
    public Guid Id { get; set; }

    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }

    public ICollection<AuthenticationToken> AuthenticationTokens { get; set; } = null!;
    public ICollection<FileReference> FileReferences { get; set; } = null!;
    public ICollection<Tenant> Tenants { get; set; } = null!;
    public ICollection<TenantUser> TenantUsers { get; set; } = null!;
    public ICollection<Project> Projects { get; set; } = null!;
    public ICollection<ProjectMember> ProjectMembers { get; set; } = null!;
    public ICollection<Project> OwnedProjects { get; set; } = null!;


}
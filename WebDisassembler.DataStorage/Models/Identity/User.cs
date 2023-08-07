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

    public ICollection<AuthenticationToken> AuthenticationTokens { get; set; }
    public ICollection<FileReference> FileReferences { get; set; }
    public ICollection<Tenant> Tenants { get; set; }
    public ICollection<TenantUser> TenantUsers { get; set; }
    public ICollection<Project> Projects { get; set; }
    public ICollection<ProjectMember> ProjectMembers { get; set; }
    public ICollection<Project> OwnedProjects { get; set; }


}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Options;

namespace WebDisassembler.DataStorage.Utility;

public class DatabaseContext : DbContext
{
    public required DbSet<Tenant> Tenants { get; set; }
    public required DbSet<User> Users { get; set; }

    public required DbSet<FileReference> FileReferences { get; set; }

    private readonly IOptions<DataStorageOptions> _options;
    
    public DatabaseContext(IOptions<DataStorageOptions> options)
    {
        _options = options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var adminUserId = Guid.Parse("a1bbbc3f-8358-4a7f-b214-9b155b6e97c4");
        var adminUser = new User()
        {
            Id = adminUserId,
            Username = "admin",
            Email = "admin@webdisassembler.io",
            PasswordHash = "123"
        };

        var adminTenantId = Guid.Parse("5a8a414f-e050-459f-aa4d-3bc41037ce4f");
        var adminTenant = new Tenant()
        {
            Id = adminTenantId,
            UserId = adminUser.Id,
            Name = "Admin Tenant",
            Subdomain = "admin",
        };

        var adminRoleId = Guid.Parse("6ad2c1f1-c06b-43c8-986b-cbaab6970c56");
        var roles = new List<Role>()
        {
            new Role()
            {
                Id = adminRoleId,
                TenantId = adminTenant.Id,
                Name = "Admin Role",
                IsAdministrator = true
            }
        };
        
        var tenantUsers = new List<TenantUser>()
        {
            new TenantUser()
            {
                UserId = adminUser.Id,
                RoleId = roles[0].Id,
                TenantId = adminTenant.Id
            }
        };
        
        modelBuilder.Entity<TenantUser>()
            .HasData(tenantUsers);
         
        modelBuilder.Entity<Role>()
            .HasData(roles);

        modelBuilder.Entity<Tenant>()
            .HasData(adminTenant);

        modelBuilder.Entity<User>()
            .HasData(adminUser);

        modelBuilder.Entity<User>()
            .HasMany(u => u.AuthenticationTokens)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .HasPrincipalKey(u => u.Id);

        modelBuilder.Entity<AuthenticationToken>()
            .HasIndex(a => a.Token)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder o)
    {
        o.LogTo(Console.WriteLine);
        switch (_options.Value.Type)
        {
            case BackendType.Sqlite:
                o.UseSqlite(_options.Value.ConnectionString);
                break;
            
            case BackendType.Postgres:
                o.UseNpgsql(_options.Value.ConnectionString);
                break;
            
            default: throw new NotImplementedException();
        }
    }
}
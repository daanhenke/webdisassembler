﻿using Microsoft.EntityFrameworkCore;
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
        modelBuilder.Entity<User>()
            .HasMany(u => u.AuthenticationTokens)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .HasPrincipalKey(u => u.Id);

        modelBuilder.Entity<AuthenticationToken>()
            .HasIndex(a => a.Token)
            .IsUnique();

        modelBuilder.Entity<Tenant>()
            .HasMany(t => t.FileReferences)
            .WithOne(f => f.Tenant)
            .HasForeignKey(f => f.TenantId)
            .HasPrincipalKey(t => t.Id);

        modelBuilder.Entity<User>()
            .HasMany(t => t.FileReferences)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId)
            .HasPrincipalKey(t => t.Id);

        modelBuilder.Entity<Role>()
            .Property(r => r.Permissions)
            .HasConversion<ListDataConverter<Permission>>();

        modelBuilder.Entity<Tenant>()
            .HasMany(t => t.Users)
            .WithMany(u => u.Tenants)
            .UsingEntity<TenantUser>(
                j => j
                    .HasOne(tu => tu.User)
                    .WithMany(u => u.TenantUsers)
                    .HasForeignKey(tu => tu.UserId),
                j => j
                    .HasOne(tu => tu.Tenant)
                    .WithMany(t => t.TenantUsers)
                    .HasForeignKey(tu => tu.TenantId),
                j => j
                    .HasKey(tu => new { tu.UserId, tu.TenantId })
            );

        modelBuilder.Entity<Tenant>()
            .HasMany(t => t.Roles)
            .WithOne(r => r.Tenant)
            .HasForeignKey(r => r.TenantId)
            .HasPrincipalKey(t => t.Id);

        modelBuilder.Entity<Tenant>()
            .HasIndex(t => t.Subdomain)
            .IsUnique();

        SeedDatabase(modelBuilder);
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

    private void SeedDatabase(ModelBuilder modelBuilder)
    {
        var adminUserId = Guid.Parse("a1bbbc3f-8358-4a7f-b214-9b155b6e97c4");
        var adminTenantId = Guid.Parse("5a8a414f-e050-459f-aa4d-3bc41037ce4f");
        var adminRoleId = Guid.Parse("6ad2c1f1-c06b-43c8-986b-cbaab6970c56");

        modelBuilder.Entity<TenantUser>()
            .HasData(new TenantUser()
            {
                UserId = adminUserId,
                RoleId = adminRoleId,
                TenantId = adminTenantId
            });
         
        modelBuilder.Entity<Role>()
            .HasData(new Role()
            {
                Id = adminRoleId,
                TenantId = adminTenantId,
                Name = "Admin Role",
                IsAdministrator = true,
                Permissions = new List<Permission>()
            });

        modelBuilder.Entity<Tenant>()
            .HasData(new Tenant()
            {
                Id = adminTenantId,
                UserId = adminUserId,
                Name = "Admin Tenant",
                Subdomain = "admin",
                Public = false
            });

        modelBuilder.Entity<User>()
            .HasData(new User()
            {
                Id = adminUserId,
                Username = "admin",
                Email = "admin@webdisassembler.io",
                PasswordHash = "123"
            });
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Options;

namespace WebDisassembler.DataStorage.Utility;

public class DatabaseContext : DbContext
{
    public required  DbSet<Project> Projects { get; set; }
    public required DbSet<Binary> Binaries { get; set; }
    public required DbSet<Section> Sections { get; set; }

    private readonly IOptions<DataStorageOptions> _options;

    public DatabaseContext(IOptions<DataStorageOptions> options)
    {
        _options = options;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder o)
    {
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
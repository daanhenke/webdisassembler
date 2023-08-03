using Microsoft.EntityFrameworkCore;
using WebDisassembler.Core.Common.Models;

namespace WebDisassembler.DataStorage.Utility;

public abstract class RepositoryBase<TModel> : IRepository<TModel> where TModel : class
{
    protected readonly DatabaseContext _database;
    protected readonly DbSet<TModel> _table;

    protected RepositoryBase(DatabaseContext database, Func<DbSet<TModel>> tableGetter)
    {
        _database = database;
        _table = tableGetter();
    }

    public void Add(TModel model)
    {
        _table.Add(model);
    }

    public void Update(TModel model)
    {
        _table.Update(model);
    }

    public void Delete(TModel model)
    {
        _table.Remove(model);
    }

    public async Task Commit()
    {
        await _database.SaveChangesAsync();
    }

    protected IQueryable<TModel> Query(bool tracked = false)
    {
        return tracked ? _table.AsTracking() : _table.AsNoTracking();
    }
}
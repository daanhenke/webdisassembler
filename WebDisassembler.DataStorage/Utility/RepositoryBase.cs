using Microsoft.EntityFrameworkCore;
using WebDisassembler.Core.Common.Models;

namespace WebDisassembler.DataStorage.Utility;

public abstract class RepositoryBase<TModel> : IRepository<TModel> where TModel : class, IIdentifiableEntity
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

    public void Track<T>(T model)
    {
        _database.Attach(model);
    }

    protected IQueryable<TModel> Query(bool tracked = false)
    {
        return tracked ? _table.AsTracking() : _table.AsNoTracking();
    }

    public async ValueTask<TModel?> Get(Guid id, bool tracked)
    {
        return await Query(tracked)
            .Where(model => model.Id == id)
            .FirstOrDefaultAsync();
    }

    public async ValueTask<TModel> GetRequired(Guid id, bool tracked)
    {
        var result = await Get(id, tracked);

        if (result == null)
        {
            throw new NotImplementedException();
        }

        return result;
    }

    public async ValueTask<IReadOnlyCollection<TModel>> GetMany(IReadOnlyCollection<Guid> ids, bool tracked)
    {
        return await Query(tracked)
            .Where(model => ids.Contains(model.Id))
            .ToListAsync();
    }
}
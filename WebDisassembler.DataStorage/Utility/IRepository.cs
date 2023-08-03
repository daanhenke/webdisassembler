namespace WebDisassembler.DataStorage.Utility;

public interface IRepository<TModel>
{
    void Add(TModel model);
    void Update(TModel model);
    void Delete(TModel model);

    ValueTask<TModel?> Get(Guid id, bool tracked);
    ValueTask<TModel> GetRequired(Guid id, bool tracked);
    ValueTask<IReadOnlyCollection<TModel>> GetMany(IReadOnlyCollection<Guid> ids, bool tracked);

    Task Commit();
}
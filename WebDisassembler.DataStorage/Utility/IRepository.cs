namespace WebDisassembler.DataStorage.Utility;

public interface IRepository<TModel>
{
    void Add(TModel model);
    void Update(TModel model);
    void Delete(TModel model);

    Task Commit();
}
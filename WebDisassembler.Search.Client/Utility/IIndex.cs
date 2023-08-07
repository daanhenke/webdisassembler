using WebDisassembler.Search.Data.Utility;

namespace WebDisassembler.Search.Client.Utility;

public interface IIndex<TModel> where TModel : IIndexedEntity
{
    ValueTask<TModel> GetById(Guid id);
}

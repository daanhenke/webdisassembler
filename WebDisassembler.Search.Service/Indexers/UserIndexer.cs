using AutoMapper;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.Search.Service.Utility;
using WebDisassemlber.Search.Data.Models;
using WebDisassemlber.Search.Data.Utility;

namespace WebDisassembler.Search.Service.Indexers;

public class UserIndexer : IndexerBase<IndexedUser>
{
    private readonly IUserRepository _userRepository;

    public UserIndexer(ElasticSearchClient client, IMapper mapper, IUserRepository userRepository) : base(client, mapper)
    {
        _userRepository = userRepository;
    }

    protected override async ValueTask<PagedResponse<IndexedUser>> FetchAllFromDatabase(PagedRequest request)
    {
        return _mapper.Map<PagedResponse<IndexedUser>>(await _userRepository.GetAllForIndex(request));
    }

    protected override async ValueTask<IReadOnlyCollection<IndexedUser>> FetchFromDatabase(ISet<Guid> ids)
    {
        return _mapper.Map<List<IndexedUser>>(await _userRepository.GetMany(ids, false));
    }
}

using AutoMapper;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.Search.Service.Utility;
using WebDisassemlber.Search.Data.Models;
using WebDisassemlber.Search.Data.Utility;

namespace WebDisassembler.Search.Service.Indexers;

public class UserIndexer : IndexerBase<User, IndexedUser>
{
    private readonly IUserRepository _userRepository;

    public UserIndexer(ElasticSearchClient client, IMapper mapper, IUserRepository userRepository) : base(client, mapper)
    {
        _userRepository = userRepository;
    }

    protected override IndexedUser Map(User model)
    {
        var indexedUser = _mapper.Map<IndexedUser>(model);
        indexedUser.IsAdministrator = model.Tenants.Any(t => t.Subdomain == "admin");

        return indexedUser;
    }

    protected override async ValueTask<PagedResponse<User>> FetchAllFromDatabase(PagedRequest request)
    {
        return await _userRepository.GetAllForIndex(request);
    }

    protected override async ValueTask<IReadOnlyCollection<User>> FetchFromDatabase(ISet<Guid> ids)
    {
        return await _userRepository.GetMany(ids, false);
    }
}

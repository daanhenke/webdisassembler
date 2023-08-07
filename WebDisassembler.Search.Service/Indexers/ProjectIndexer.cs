using AutoMapper;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.Search.Service.Utility;
using WebDisassemlber.Search.Data.Models;
using WebDisassemlber.Search.Data.Utility;

namespace WebDisassembler.Search.Service.Indexers;

public class ProjectIndexer : IndexerBase<IndexedProject>
{
    private readonly IProjectRepository _projectRepository;

    public ProjectIndexer(ElasticSearchClient client, IMapper mapper, IProjectRepository projectRepository) : base(client, mapper)
    {
        _projectRepository = projectRepository;
    }

    protected override async ValueTask<PagedResponse<IndexedProject>> FetchAllFromDatabase(PagedRequest request)
    {
        return _mapper.Map<PagedResponse<IndexedProject>>(await _projectRepository.GetAllForIndex(request));
    }

    protected override async ValueTask<IReadOnlyCollection<IndexedProject>> FetchFromDatabase(ISet<Guid> ids)
    {
        return _mapper.Map<List<IndexedProject>>(await _projectRepository.GetMany(ids, false));
    }
}

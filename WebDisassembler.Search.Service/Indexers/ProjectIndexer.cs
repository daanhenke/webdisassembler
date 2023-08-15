using AutoMapper;
using Elastic.Clients.Elasticsearch.Mapping;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models.Projects;
using WebDisassembler.DataStorage.Repositories;
using WebDisassembler.Search.Data.Utility;
using WebDisassembler.Search.Service.Utility;
using WebDisassemlber.Search.Data.Models;

namespace WebDisassembler.Search.Service.Indexers;

public class ProjectIndexer : IndexerBase<Project, IndexedProject>
{
    private readonly IProjectRepository _projectRepository;

    public ProjectIndexer(ElasticSearchClient client, IMapper mapper, IProjectRepository projectRepository) : base(client, mapper)
    {
        _projectRepository = projectRepository;
    }

    protected override void SetIndexMapping(PropertiesDescriptor<IndexedProject> descriptor)
        => descriptor
            .Keyword(p => p.UserId);

    protected override IndexedProject Map(Project model)
    {
        var indexedModel = _mapper.Map<IndexedProject>(model);
        indexedModel.FileTree = new();

        foreach (var binary in model.Binaries)
        {
            var pathEntries = binary
                .ProjectPath
                .Split('/')
                .Select(e => e.Trim())
                .ToList();

            var parent = indexedModel.FileTree;
            for (var i = 0; i < pathEntries.Count; i++)
            {
                var chunk = pathEntries[i];
                var isLastChunk = i == pathEntries.Count - 1;

                if (!isLastChunk)
                {
                    if (parent.ContainsKey(chunk))
                    {
                        parent = (Dictionary<string, object>)parent[chunk];
                        continue;
                    }

                    var folder = new Dictionary<string, object>();
                    parent[chunk] = folder;
                    parent = folder;
                }
                else
                {
                    parent[chunk] = binary.Id;
                }
            }
        }

        return indexedModel;
    }

    protected override async ValueTask<PagedResponse<Project>> FetchAllFromDatabase(PagedRequest request)
    {
        return await _projectRepository.GetAllForIndex(request);
    }

    protected override async ValueTask<IReadOnlyCollection<Project>> FetchFromDatabase(ISet<Guid> ids)
    {
        return await _projectRepository.GetForIndex(ids, false);
    }
}

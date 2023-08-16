using WebDisassembler.Core.Common.Models;
using WebDisassembler.Search.Client.Utility;
using WebDisassembler.Search.Data.Utility;
using WebDisassemlber.Search.Data.Models;

namespace WebDisassembler.Search.Client.Indices.Impl;

public class ProjectIndex : IndexBase<IndexedProject>, IProjectIndex
{
    public ProjectIndex(ElasticSearchClient client) : base(client)
    {
    }

    public async ValueTask<PagedResponse<IndexedProject>> FindForUser(Guid userId, PagedRequest request)
    {
        return await _client.FindPaged<IndexedProject>(request, q => q
            .Bool(b => b
                .Should(
                    m => m.Term(p => p.UserId, userId),
                    m => m.Term(p => p.ProjectMembers, userId)
                )
            )
        );
    }
}
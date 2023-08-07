using WebDisassembler.ServiceProtocol.Clients;

namespace WebDisassembler.Core.Application.Services.Admin;

public class SearchService
{
    private readonly ISearchServiceClient _searchServiceClient;

    public SearchService(ISearchServiceClient searchServiceClient)
    {
        _searchServiceClient = searchServiceClient;
    }

    public async ValueTask ReindexAll()
    {
        await _searchServiceClient.IndexAllRecords(new HashSet<string>()
        {
            "User", "Tenant", "Project"
        });
    }
}

using MassTransit;
using WebDisassembler.ServiceProtocol.Contracts;
using WebDisassembler.ServiceProtocol.Utility;

namespace WebDisassembler.ServiceProtocol.Clients;

public interface ISearchServiceClient
{
    ValueTask<IndexAllRecordsResponse> IndexAllRecords(HashSet<string> indices);    ValueTask<IndexUsersResponse> IndexUsers(List<Guid> userIds);    ValueTask<IndexTenantsResponse> IndexTenants(List<Guid> tenantIds);
}

public class SearchServiceClient : ServiceClientBase, ISearchServiceClient
{
    public SearchServiceClient(IScopedClientFactory clientFactory) : base(clientFactory) {}
    
    public async ValueTask<IndexAllRecordsResponse> IndexAllRecords(HashSet<string> indices) => await SendRequest<IndexAllRecordsRequest, IndexAllRecordsResponse>(new(indices));    public async ValueTask<IndexUsersResponse> IndexUsers(List<Guid> userIds) => await SendRequest<IndexUsersRequest, IndexUsersResponse>(new(userIds));    public async ValueTask<IndexTenantsResponse> IndexTenants(List<Guid> tenantIds) => await SendRequest<IndexTenantsRequest, IndexTenantsResponse>(new(tenantIds));
}
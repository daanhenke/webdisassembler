using MassTransit;
using WebDisassembler.Core.ServiceProtocol.Contracts;
using WebDisassembler.Core.ServiceProtocol.Utility;

namespace WebDisassembler.Core.ServiceProtocol.Clients;

public interface ISearchServiceClient
{
    ValueTask<IndexAllRecordsResponse> IndexAllRecords(HashSet<string> indices);    ValueTask<IndexUsersResponse> IndexUsers(List<Guid> userIds);    ValueTask<IndexTenantsResponse> IndexTenants(List<Guid> tenantIds);    ValueTask<IndexProjectsResponse> IndexProjects(List<Guid> projectIds);
}

public class SearchServiceClient : ServiceClientBase, ISearchServiceClient
{
    public SearchServiceClient(IScopedClientFactory clientFactory) : base(clientFactory) {}
    
    public async ValueTask<IndexAllRecordsResponse> IndexAllRecords(HashSet<string> indices) => await SendRequest<IndexAllRecordsRequest, IndexAllRecordsResponse>(new(indices));    public async ValueTask<IndexUsersResponse> IndexUsers(List<Guid> userIds) => await SendRequest<IndexUsersRequest, IndexUsersResponse>(new(userIds));    public async ValueTask<IndexTenantsResponse> IndexTenants(List<Guid> tenantIds) => await SendRequest<IndexTenantsRequest, IndexTenantsResponse>(new(tenantIds));    public async ValueTask<IndexProjectsResponse> IndexProjects(List<Guid> projectIds) => await SendRequest<IndexProjectsRequest, IndexProjectsResponse>(new(projectIds));
}
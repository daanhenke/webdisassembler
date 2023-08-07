namespace WebDisassembler.ServiceProtocol.Contracts;

public record IndexAllRecordsRequest(HashSet<string> Indices);
public record IndexAllRecordsResponse();

public record IndexUsersRequest(List<Guid> UserIds);
public record IndexUsersResponse();

public record IndexTenantsRequest(List<Guid> TenantIds);
public record IndexTenantsResponse();

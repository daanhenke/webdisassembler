using MassTransit;
using WebDisassembler.ServiceProtocol.Contracts;
using WebDisassembler.ServiceProtocol.Utility;

namespace WebDisassembler.ServiceProtocol.Clients;

public interface IAnalysisServiceClient
{
    ValueTask<StartAnalyzeBinaryResponse> StartAnalyzeBinary(Guid binaryId);
}

public class AnalysisServiceClient : ServiceClientBase, IAnalysisServiceClient
{
    public AnalysisServiceClient(IScopedClientFactory clientFactory) : base(clientFactory) {}
    
    public async ValueTask<StartAnalyzeBinaryResponse> StartAnalyzeBinary(Guid binaryId) => await SendRequest<StartAnalyzeBinaryRequest, StartAnalyzeBinaryResponse>(new(binaryId));
}
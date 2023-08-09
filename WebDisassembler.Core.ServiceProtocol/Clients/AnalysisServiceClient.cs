using MassTransit;
using WebDisassembler.Core.ServiceProtocol.Contracts;
using WebDisassembler.Core.ServiceProtocol.Utility;

namespace WebDisassembler.Core.ServiceProtocol.Clients;

public interface IAnalysisServiceClient
{
    ValueTask<StartBinaryAnalysisResponse> StartBinaryAnalysis(Guid projectId, Guid binaryId);
}

public class AnalysisServiceClient : ServiceClientBase, IAnalysisServiceClient
{
    public AnalysisServiceClient(IScopedClientFactory clientFactory) : base(clientFactory) {}
    
    public async ValueTask<StartBinaryAnalysisResponse> StartBinaryAnalysis(Guid projectId, Guid binaryId) => await SendRequest<StartBinaryAnalysisRequest, StartBinaryAnalysisResponse>(new(projectId, binaryId));
}
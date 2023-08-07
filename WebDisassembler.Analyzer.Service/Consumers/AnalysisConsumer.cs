using MassTransit;
using WebDisassembler.Core.ServiceProtocol.Contracts;

namespace WebDisassembler.Analyzer.Service.Consumers;

public class AnalysisConsumer :
    IConsumer<StartBinaryAnalysisRequest>
{
    public async Task Consume(ConsumeContext<StartBinaryAnalysisRequest> context)
    {
        await context.RespondAsync(new StartBinaryAnalysisResponse());
    }
}
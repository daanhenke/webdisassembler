using MassTransit;
using WebDisassembler.ServiceProtocol.Contracts;

namespace WebDisassembler.Analyzer.Service.Consumers;

public class AnalysisConsumer :
    IConsumer<StartAnalyzeBinaryRequest>
{
    public async Task Consume(ConsumeContext<StartAnalyzeBinaryRequest> context)
    {
        await context.RespondAsync(new StartAnalyzeBinaryResponse());
    }
}
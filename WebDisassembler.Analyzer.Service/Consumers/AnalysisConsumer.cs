using MassTransit;
using WebDisassembler.Analyzer.Core.Pipeline;
using WebDisassembler.Core.ServiceProtocol.Contracts;

namespace WebDisassembler.Analyzer.Service.Consumers;

public class AnalysisConsumer :
    IConsumer<StartBinaryAnalysisRequest>
{
    private readonly AnalysisPipeline _analysisPipeline;

    public AnalysisConsumer(AnalysisPipeline analysisPipeline)
    {
        _analysisPipeline = analysisPipeline;
    }

    public async Task Consume(ConsumeContext<StartBinaryAnalysisRequest> context)
    {
        await context.RespondAsync(new StartBinaryAnalysisResponse());
        await _analysisPipeline.StartAnalysis(context.Message.ProjectId, context.Message.BinaryId);
    }
}
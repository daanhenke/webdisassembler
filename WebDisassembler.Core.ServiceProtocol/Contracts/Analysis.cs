namespace WebDisassembler.Core.ServiceProtocol.Contracts;

public record StartBinaryAnalysisRequest(Guid BinaryId);
public record StartBinaryAnalysisResponse();
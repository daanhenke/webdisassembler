namespace WebDisassembler.Core.ServiceProtocol.Contracts;

public record StartBinaryAnalysisRequest(Guid ProjectId, Guid BinaryId);

public record StartBinaryAnalysisResponse();
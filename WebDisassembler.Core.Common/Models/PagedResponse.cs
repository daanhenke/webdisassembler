namespace WebDisassembler.Core.Common.Models;

public record PagedResponse<TResponse>(int Total, TResponse[] Items);
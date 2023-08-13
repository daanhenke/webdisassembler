using System.ComponentModel;

namespace WebDisassembler.Core.Common.Models;

public record QueryRequest(int Index, [property: DefaultValue(10)] int Size, string? Query);

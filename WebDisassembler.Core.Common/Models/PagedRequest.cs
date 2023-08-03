using System.ComponentModel;

namespace WebDisassembler.Core.Common.Models;

public record PagedRequest(int Index, [property: DefaultValue(10)] int Size);
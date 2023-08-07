namespace WebDisassembler.Core.Application.Models.Binaries;

public record CreateBinary(
    string ProjectPath,
    Guid FileId
);

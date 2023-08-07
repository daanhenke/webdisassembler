namespace WebDisassembler.Analyzer.Core;

public interface IFileFormatLoader
{
    ValueTask<bool> IsFileSupported(Stream stream);
    ValueTask Analyze(Stream stream);
}
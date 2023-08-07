using WebDisassembler.Analyzer.Core;

namespace WebDisassembler.Loader.PortableExecutable;

public class PortableExecutableLoader : IFileFormatLoader
{
    public async ValueTask<bool> IsFileSupported(Stream stream)
    {
        throw new NotImplementedException();
    }

    public async ValueTask Analyze(Stream stream)
    {
        throw new NotImplementedException();
    }
}
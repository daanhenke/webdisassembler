using System.Reflection;
using Microsoft.Extensions.Logging;

namespace WebDisassembler.CodeGenerator.Utility;

public abstract class GeneratorBase<T> : IGenerator where T : GeneratorBase<T>
{
    protected readonly ILogger _logger;

    protected GeneratorBase()
    {
        _logger = LoggerFactory.Create(c =>
        {
            c.AddConsole();
        }).CreateLogger<T>();
    }

    public abstract ValueTask Generate();

    private const int SolutionDirParentCount = 5;
    protected string GetSolutionPath()
    {
        var result = Assembly
            .GetExecutingAssembly()
            .Location;

        for (var i = 0; i < SolutionDirParentCount; i++)
        {
            result = Path.GetDirectoryName(result);
        }

        return result!;
    }
    
    protected string ToCamelCase(string str)
    {                    
        if(!string.IsNullOrEmpty(str) && str.Length > 1)
        {
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
        
        return str.ToLowerInvariant();
    }

    private readonly List<string> _binDirs = new();
    private bool _hasAttachedResolver = false;
    protected Assembly LoadSolutionProject(string projectName)
    {
        var projectBinPath = Path.Combine(
            GetSolutionPath(),
            projectName,
            "bin",
            "Debug",
            "net7.0"
        );

        _binDirs.Add(projectBinPath);

        if (! _hasAttachedResolver)
        {
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssemblies;
            _hasAttachedResolver = true;
        }

        return Assembly.Load(projectName);
    }

    private Assembly? ResolveAssemblies(object? sender, ResolveEventArgs args)
    {
        foreach (var binPath in _binDirs)
        {
            var assemblyPath = Path.Combine(binPath, args.Name.Split(',').First()) + ".dll";

            if (File.Exists(assemblyPath))
            {
                return Assembly.LoadFile(assemblyPath);
            }
        }

        return null;
    }
}
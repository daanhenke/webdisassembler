using System.Reflection;
using Microsoft.Extensions.Logging;
using Scriban;

namespace WebDisassembler.CodeGenerator.Generators;

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

        return result;
    }

    private static Assembly _sourceGeneratorAssembly = Assembly.GetExecutingAssembly();
    private static string _templatesNamespace = "WebDisassembler.CodeGenerator.Templates";
    protected async ValueTask<string> RenderTemplateToString(string templateName, object state)
    {
        var templateResourceStream =
            _sourceGeneratorAssembly.GetManifestResourceStream($"{_templatesNamespace}.{templateName}.liquid");

        if (templateResourceStream == null)
        {
            throw new NotImplementedException();
        }

        using var reader = new StreamReader(templateResourceStream);
        var templateContent = await reader.ReadToEndAsync();
        var template = Template.ParseLiquid(templateContent);

        return await template.RenderAsync(state);
    }
    
    protected string ToCamelCase(string str)
    {                    
        if(!string.IsNullOrEmpty(str) && str.Length > 1)
        {
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
        
        return str.ToLowerInvariant();
    }
}
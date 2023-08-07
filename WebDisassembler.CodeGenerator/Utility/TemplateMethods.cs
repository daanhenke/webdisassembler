using System.Reflection;
using Scriban;
using Scriban.Runtime;

namespace WebDisassembler.CodeGenerator.Utility;

public static class TemplateMethods
{
    private readonly static Assembly _sourceGeneratorAssembly = Assembly.GetExecutingAssembly();
    private readonly static string _templatesNamespace = "WebDisassembler.Tools.CodeGenerator.Templates";
    
    public static async ValueTask<string> RenderTemplate(string templateName, Dictionary<string, object>? templateParameters)
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

        var scriptObject = new ScriptObject();
        scriptObject.Import(typeof(TemplateMethods));
        if (templateParameters != null)
        {
            foreach (var (key, value) in templateParameters)
            {
                scriptObject[key] = value;
            }
        }

        var context = new TemplateContext();
        context.PushGlobal(scriptObject);

        return await template.RenderAsync(context);
    }

    public static async ValueTask RenderTemplateToFile(string templateName, string filePath, Dictionary<string, object>? templateParameters)
    {
        await File.WriteAllTextAsync(filePath, await RenderTemplate(templateName, templateParameters));
    }
}

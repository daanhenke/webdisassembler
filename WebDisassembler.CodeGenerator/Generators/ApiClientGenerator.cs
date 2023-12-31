using Microsoft.AspNetCore.Mvc;
using WebDisassembler.CodeGenerator.Models;
using WebDisassembler.CodeGenerator.Utility;

namespace WebDisassembler.CodeGenerator.Generators;

public class ApiClientGenerator : GeneratorBase<ApiClientGenerator>
{
    private Dictionary<string, ApiModel> _models = new();
    private List<ApiClient> _clients = new();

    public async override ValueTask Generate()
    {
        await AnalyzeApiAssembly();

        var modelsByNamespace = _models.Values.ToLookup(model => model.Namespace);
        var clientApiDir = Path.Combine(GetSolutionPath(), "WebDisassembler.Api", "ClientApp", "source", "api");
        var modelsDir = Path.Combine(clientApiDir, "models");
        var httpClientsDir = Path.Combine(clientApiDir, "http");

        foreach (var pair in modelsByNamespace)
        {
            var ns = pair.Key;
            var models = pair.ToList()!;
            var dependencies = ResolveImports(models.SelectMany(m => m.Properties.Select(p => p.Type)), ns);

            await TemplateMethods.RenderTemplateToFile("TypescriptModel", Path.Combine(modelsDir, $"{ns}.ts"), new Dictionary<string, object>()
            {
                ["namespace"] = ns,
                ["models"] = models,
                ["dependencies"] = dependencies
            });
        }

        foreach (var client in _clients)
        {
            var modelNames = client.Methods.SelectMany(m => m.Parameters.Select(p => p.Type)).ToList();
            modelNames.AddRange(client.Methods.Select(m => m.ReturnType));

            var models = modelNames.Where(name => _models.ContainsKey(name)).Select(name => _models[name]).ToList();
            modelNames.AddRange(models.SelectMany(model => model.Generics));
            
            var dependencies = ResolveImports(modelNames);
            await TemplateMethods.RenderTemplateToFile("TypescriptHttpClient", Path.Combine(httpClientsDir, $"{ToCamelCase(client.Name)}.ts"), new Dictionary<string, object>()
            {
                ["client"] = client,
                ["dependencies"] = dependencies
            });
        }
    }

    private Dictionary<string, string> ResolveImports(IEnumerable<string> models, string ns = "")
    {
        return models
            .Where(typeName => _models.ContainsKey(typeName))
            .Select(typeName => _models[typeName])
            .Where(type => type.Namespace != ns)
            .ToLookup(type => type.Namespace)
            .ToDictionary(kv => $"@/api/models/{kv.Key}", kv => string.Join(", ", kv.Select(p => p.Name)));
    }

    private static Type[] _attributeTypes = new[]
    {
        typeof(HttpGetAttribute),
        typeof(HttpPostAttribute),
        typeof(HttpPutAttribute),
        typeof(HttpDeleteAttribute)
    };

    private async ValueTask AnalyzeApiAssembly()
    {
        var assembly = LoadSolutionProject("WebDisassembler.Api");

        var httpControllers = assembly.ExportedTypes
            .Where(type => type.BaseType == typeof(ControllerBase));

        foreach (var controller in httpControllers)
        {
            await AnalyzeHttpController(controller);
        }
    }

    private async ValueTask AnalyzeHttpController(Type controllerType)
    {
        var client = new ApiClient()
        {
            Name = controllerType.Name.Replace("Controller", "")
        };

        var methods = controllerType.GetMethods()
            .Where(method => method.CustomAttributes.Any(attr => _attributeTypes.Contains(attr.AttributeType)));

        foreach (var method in methods) 
        {
            string returnType = null!;
            List<ApiNamedType> parameters = new();
            
            var methodParameters = method.GetParameters();
            var methodModels = methodParameters.Select(param => param.ParameterType).ToList();
            methodModels.Add(method.ReturnType);

            foreach (var model in methodModels)
            {
                var name = await AnalyzeModel(model);
                if (method.ReturnType == model)
                {
                    returnType = name;
                }
                else
                {
                    parameters.Add(new()
                    {
                        Name = ToCamelCase(methodParameters.First(p => p.ParameterType == model).Name!),
                        Type = name,
                        IsArray = model.IsArray
                    });
                }
            }

            client.Methods.Add(new()
            {
                Name = method.Name,
                ReturnType = returnType,
                Parameters = parameters
            });
        }

        _clients.Add(client);
    }

    private readonly static string[] _nsWhitelist =
    {
        "System",
        "WebDisassembler",
        "Microsoft.AspNetCore.Http.IFormFile"
    };

    private readonly static Type[] _wrapperTypes =
    {
        typeof(Task<>),
        typeof(ValueTask<>),
        typeof(Nullable<>)
    };

    private readonly static Type[] _arrayTypes =
    {
        typeof(List<>),
        typeof(HashSet<>)
    };

    private readonly static Dictionary<string, string> _typeMap = new()
    {
        ["System.Int32"] = "number",
        ["System.Int64"] = "number",
        ["System.String"] = "string",
        ["System.Guid"] = "string",
        ["System.Object"] = "object",
        ["System.Boolean"] = "boolean",
        ["Microsoft.AspNetCore.Mvc.ActionResult"] = "void",
        ["Microsoft.AspNetCore.Http.IFormFile"] = "Blob"
    };

    private async ValueTask<string> AnalyzeModel(Type modelType)
    {
        if (modelType.IsGenericType && _wrapperTypes.Contains(modelType.GetGenericTypeDefinition()))
        {
            modelType = modelType.GenericTypeArguments[0];
        }

        if (modelType.HasElementType)
        {
            modelType = modelType.GetElementType()!;
        }

        if (modelType.IsGenericType && _arrayTypes.Contains(modelType.GetGenericTypeDefinition()))
        {
            modelType = modelType.GenericTypeArguments[0];
        }

        var realType = modelType;
        if (modelType.IsGenericType)
        {
            modelType = modelType.GetGenericTypeDefinition();
        }

        var ns = modelType.Namespace!;
        if (! ns.StartsWith("WebDisassembler"))
        {
            return _typeMap[modelType.FullName!];
        }

        var generics = new List<string>();
        var genericNames = new List<string>();
        var tsName = modelType.Name;
        var fullName = tsName;
        if (modelType.IsGenericType)
        {
            var genericTasks = realType
                .GenericTypeArguments
                .Select(type => AnalyzeModel(type).AsTask())
                .ToList();
            var genericNameTasks = modelType
                .GetGenericTypeDefinition()
                .GetGenericArguments()
                .Select(type => AnalyzeModel(type).AsTask())
                .ToList();
            await Task.WhenAll(genericTasks);
            await Task.WhenAll(genericNameTasks);
            generics = genericTasks.Select(task => task.Result).ToList();
            genericNames = genericNameTasks.Select(task => task.Result).ToList();
            
            tsName = tsName.Split('`').First();
            fullName = $"{tsName}<{string.Join(", ", generics)}>";
        }

        if (_models.ContainsKey(fullName))
        {
            return fullName;
        }

        var tsNamespace = ToCamelCase(modelType.Assembly
            .GetName()
            .Name!
            .Split('.')
            .Last()
        );

        var model = new ApiModel()
        {
            Name = tsName,
            FullName = fullName,
            Namespace = tsNamespace,
            CsharpName = modelType.FullName!,
            Generics = generics,
            GenericNames = genericNames
        };
        
        foreach (var prop in modelType.GetProperties())
        {
            var tsType = prop.PropertyType.Name;
            if (! model.GenericNames.Contains(tsName.Split('[').First()))
            {
                tsType = await AnalyzeModel(prop.PropertyType);
            }

            var genericProp = prop.PropertyType.IsGenericType
                ? prop.PropertyType.GetGenericTypeDefinition()
                : prop.PropertyType;
            model.Properties.Add(new()
            {
                Type = tsType,
                Name = ToCamelCase(prop.Name),
                IsArray = prop.PropertyType.IsArray || prop.PropertyType.HasElementType || _arrayTypes.Contains(genericProp)
            });
        }

        _models.Add(fullName, model);
        return fullName;
    }
}

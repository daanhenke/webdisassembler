namespace WebDisassembler.CodeGenerator.Models;

public class ApiModel
{
    public required string Name { get; set; }
    public required string CsharpName { get; set; }
    public required string Namespace { get; set; }

    public List<ApiNamedType> Properties { get; set; } = new();
}

public class ApiNamedType
{
    public required string Name { get; set;}
    public required string Type { get; set; }
}

public class ApiClient
{
    public required string Name { get; set; }
    public List<ApiMethod> Methods { get; set; } = new();
}

public class ApiMethod
{
    public required string Name { get; set; }
    public required string ReturnType { get; set; }
    public List<ApiNamedType> Parameters { get; set; } = new();

    public string PrototypeArguments => string.Join(", ", Parameters.Select(p => $"{p.Name}: {p.Type}"));
}
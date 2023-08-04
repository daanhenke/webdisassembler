namespace WebDisassembler.CodeGenerator.Models;

public class ServiceClient
{
    public string Name { get; set; }
    
    public List<ServiceEvent> Events { get; set; } = new();
    public List<ServiceMethod> Methods { get; set; } = new();
    public List<string> Usings { get; set; } = new();
}
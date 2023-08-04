namespace WebDisassembler.CodeGenerator.Models;

public class ServiceFunctor
{
    public ServiceFunctor(string name, List<ServiceArgument> arguments)
    {
        Name = name;
        Arguments = arguments;
        ArgumentsForPrototype = string.Join(", ", arguments.Select(arg => $"{arg.Type} {arg.Name}"));
        ArgumentsForCall = string.Join(", ", arguments.Select(arg => arg.Name));
    }

    public string Name { get; set; }
    public List<ServiceArgument> Arguments { get; set; }

    public string ArgumentsForPrototype { get; init; }
    public string ArgumentsForCall { get; init; }
}
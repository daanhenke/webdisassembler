namespace WebDisassembler.CodeGenerator.Models;

public class ServiceMethod : ServiceFunctor
{
    public ServiceMethod(string name, List<ServiceArgument> arguments) : base(name, arguments) { }
}
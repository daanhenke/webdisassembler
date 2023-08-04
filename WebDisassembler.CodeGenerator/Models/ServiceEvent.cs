namespace WebDisassembler.CodeGenerator.Models;

public class ServiceEvent : ServiceFunctor
{
    public ServiceEvent(string name, List<ServiceArgument> arguments) : base(name, arguments) { }
}
namespace WebDisassembler.CodeGenerator.Generators;

public interface IGenerator
{
    ValueTask Generate();
}
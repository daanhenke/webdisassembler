using WebDisassembler.CodeGenerator.Generators;

var generators = new List<IGenerator>()
{
    new ServiceClientGenerator()
};

foreach (var generator in generators)
{
    await generator.Generate();
}
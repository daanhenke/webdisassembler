using WebDisassembler.CodeGenerator.Generators;
using WebDisassembler.CodeGenerator.Utility;

var generators = new List<IGenerator>()
{
    new ServiceClientGenerator(),
    //new ApiClientGenerator()
};

foreach (var generator in generators)
{
    await generator.Generate();
}
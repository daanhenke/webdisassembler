using Microsoft.Extensions.Logging;
using WebDisassembler.CodeGenerator.Models;
using WebDisassembler.CodeGenerator.Utility;

namespace WebDisassembler.CodeGenerator.Generators;

public class ServiceClientGenerator : GeneratorBase<ServiceClientGenerator>
{
    private readonly List<ServiceClient> _serviceClients = new();

    public override async ValueTask Generate()
    {
        var projectPath = Path.Combine(GetSolutionPath(), "WebDisassembler.Core.ServiceProtocol");
        AnalyzeContracts(projectPath);

        var clientsPath = Path.Combine(projectPath, "Clients");
        foreach (var client in _serviceClients)
        {
            await TemplateMethods.RenderTemplateToFile("ServiceClient", Path.Combine(clientsPath, $"{client.Name}ServiceClient.cs"), new Dictionary<string, object>()
            {
                { "client", client }
            });
        }
    }

    private void AnalyzeContracts(string projectPath)
    {
        var contractsDir = Path.Combine(projectPath, "Contracts");
        var contractFiles = Directory
            .EnumerateFiles(contractsDir)
            .Where(path => path.EndsWith(".cs"))
            .ToHashSet();

        foreach (var contractFilePath in contractFiles)
        {
            _serviceClients.Add(AnalyzeContract(contractFilePath));
        }
    }

    private ServiceClient AnalyzeContract(string contractFilePath)
    {

        var fileName = Path.GetFileName(contractFilePath)
            .Split('.')
            .First();
        var lines = File.ReadAllLines(contractFilePath)
            .Select(l => l.Trim());

        var result = new ServiceClient()
        {
            Name = fileName
        };

        result.Usings.Add("MassTransit");
        result.Usings.Add("WebDisassembler.Core.ServiceProtocol.Contracts");
        result.Usings.Add("WebDisassembler.Core.ServiceProtocol.Utility");

        foreach (var line in lines)
        {
            var chunks = line.Split(new [] { ' ', '(', ')' }).ToList();
            if (line.Contains("record"))
            {
                var recordIndex = chunks.FindIndex(c => c == "record");
                var recordName = chunks[recordIndex + 1];
                var arguments = new List<ServiceArgument>();

                var currentIndex = recordIndex + 2;
                while (chunks[currentIndex] != ";" && chunks[currentIndex].Length > 0)
                {
                    var type = chunks[currentIndex++];
                    var name = chunks[currentIndex++];
                    arguments.Add(new() { Type = type, Name = ToCamelCase(name) });
                }
                
                if (recordName.EndsWith("Request"))
                {
                    result.Methods.Add(
                        new(recordName.Substring(0, recordName.IndexOf("Request")), arguments)
                    );
                }
            }
        }

        return result;
    }
}
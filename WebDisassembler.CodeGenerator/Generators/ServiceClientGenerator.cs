﻿using Microsoft.Extensions.Logging;
using WebDisassembler.CodeGenerator.Models;

namespace WebDisassembler.CodeGenerator.Generators;

public class ServiceClientGenerator : GeneratorBase<ServiceClientGenerator>
{
    private readonly List<ServiceClient> _serviceClients = new();

    public override async ValueTask Generate()
    {
        var projectPath = Path.Combine(GetSolutionPath(), "WebDisassembler.ServiceProtocol");
        await AnalyzeContracts(projectPath);

        var clientsPath = Path.Combine(projectPath, "Clients");
        foreach (var client in _serviceClients)
        {
            var fileName = $"{client.Name}ServiceClient.cs";
            var content = await RenderTemplateToString("ServiceClient", client);
            await File.WriteAllTextAsync(Path.Combine(clientsPath, fileName), content);
        }
    }

    private async ValueTask AnalyzeContracts(string projectPath)
    {
        var contractsDir = Path.Combine(projectPath, "Contracts");
        var contractFiles = Directory
            .EnumerateFiles(contractsDir)
            .Where(path => path.EndsWith(".cs"))
            .ToHashSet();

        foreach (var contractFilePath in contractFiles)
        {
            _serviceClients.Add(await AnalyzeContract(contractFilePath));
        }
    }

    private async ValueTask<ServiceClient> AnalyzeContract(string contractFilePath)
    {
        var result = new ServiceClient();

        var fileName = Path.GetFileName(contractFilePath)
            .Split('.')
            .First();
        var lines = File.ReadAllLines(contractFilePath)
            .Select(l => l.Trim());

        result.Name = fileName;
        result.Usings.Add("MassTransit");
        result.Usings.Add("WebDisassembler.ServiceProtocol.Contracts");
        result.Usings.Add("WebDisassembler.ServiceProtocol.Utility");

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
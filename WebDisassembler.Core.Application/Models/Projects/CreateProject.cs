namespace WebDisassembler.Core.Application.Models.Projects;

public record CreateProject(Guid TenantId, string Name, string ShortDescription);
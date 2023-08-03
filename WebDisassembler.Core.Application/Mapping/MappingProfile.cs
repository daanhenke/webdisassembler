using AutoMapper;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Core.Models.Projects;
using WebDisassembler.DataStorage.Models;

namespace WebDisassembler.Core.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Project, ProjectSummary>();
        CreateMap<Binary, BinarySummary>();

        CreateMap(typeof(PagedResponse<>), typeof(PagedResponse<>));
    }
}
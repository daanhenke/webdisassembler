using AutoMapper;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Core.Models.Authentication;
using WebDisassembler.Core.Models.Projects;
using WebDisassembler.DataStorage.Models;
using WebDisassembler.DataStorage.Models.Identity;

namespace WebDisassembler.Core.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateMap<Project, ProjectSummary>();
        // CreateMap<Binary, BinarySummary>();

        CreateMap(typeof(PagedResponse<>), typeof(PagedResponse<>));
        CreateMap<User, CurrentUser>()
            .ForMember(u => u.UserId, m => m.MapFrom(u => u.Id));
    }
}
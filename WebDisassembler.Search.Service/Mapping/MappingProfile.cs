using AutoMapper;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Models.Projects;
using WebDisassemlber.Search.Data.Models;

namespace WebDisassembler.Search.Service.Mapping;

class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap(typeof(PagedResponse<>), typeof(PagedResponse<>));
        
        CreateMap<User, IndexedUser>();
        CreateMap<Tenant, IndexedUserTenant>();

        CreateMap<Tenant, IndexedTenant>();
        
        CreateMap<Project, IndexedProject>()
            .ForMember(p => p.BinaryNames, m => m.MapFrom(p => p.Binaries.Select(b => b.ProjectPath)))
            .ForMember(p => p.FileTree, m => m.Ignore());
        CreateMap<ProjectMember, IndexedProjectMember>();
    }
}
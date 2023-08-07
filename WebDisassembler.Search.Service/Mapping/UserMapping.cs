using AutoMapper;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Models.Projects;
using WebDisassemlber.Search.Data.Models;

namespace WebDisassembler.Search.Service.Mapping;

class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap(typeof(PagedResponse<>), typeof(PagedResponse<>));
        CreateMap<User, IndexedUser>();
        CreateMap<Tenant, IndexedTenant>();
        
        CreateMap<Project, IndexedProject>()
            .ForMember(p => p.BinaryNames, m => m.MapFrom(p => p.Binaries.Select(b => b.Filename)));
        CreateMap<ProjectMember, IndexedProjectMember>();
    }
}
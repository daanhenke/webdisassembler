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

        CreateMap<Tenant, IndexedTenant>()
            .ForMember(t => t.Users, 
                m => m.MapFrom(t => t.TenantUsers));
        CreateMap<TenantUser, IndexedTenantUser>();
        
        CreateMap<Project, IndexedProject>()
            .ForMember(p => p.BinaryNames, m => m.MapFrom(p => p.Binaries.Select(b => b.ProjectPath)))
            .ForMember(p => p.FileTree, m => m.Ignore());
        CreateMap<ProjectMember, IndexedProjectMember>();
    }
}
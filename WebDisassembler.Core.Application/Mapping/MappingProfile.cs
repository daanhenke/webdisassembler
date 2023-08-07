using AutoMapper;
using WebDisassembler.Core.Application.Models;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.Core.Models.Authentication;
using WebDisassembler.Core.Models.Identity;
using WebDisassembler.Core.Models.Projects;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Models.Projects;
using WebDisassemlber.Search.Data.Models;

namespace WebDisassembler.Core.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap(typeof(PagedResponse<>), typeof(PagedResponse<>));

        CreateMap<User, CurrentUser>()
            .ForMember(u => u.UserId, m => m.MapFrom(u => u.Id))
            .ForMember(u => u.Tenants, m => m.MapFrom(u => u.Tenants));

        CreateMap<Tenant, CurrentUserTenant>()
            .ForMember(t => t.TenantId, m => m.MapFrom(t => t.Id));

        CreateMap<CreateTenant, Tenant>();
        CreateMap<IndexedTenant, TenantSummary>();
        
        CreateMap<CreateProject, Project>();
    }
}
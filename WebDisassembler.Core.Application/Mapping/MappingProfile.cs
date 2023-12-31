using AutoMapper;
using WebDisassembler.Core.Application.Models;
using WebDisassembler.Core.Application.Models.Admin;
using WebDisassembler.Core.Application.Models.Authentication;
using WebDisassembler.Core.Application.Models.Binaries;
using WebDisassembler.Core.Application.Models.Identity;
using WebDisassembler.Core.Application.Models.Projects;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassembler.DataStorage.Models.Projects;
using WebDisassemlber.Search.Data.Models;

namespace WebDisassembler.Core.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap(typeof(PagedResponse<>), typeof(PagedResponse<>));

        CreateMap<IndexedUser, CurrentUser>()
            .ForMember(u => u.UserId, m => m.MapFrom(u => u.Id));

        CreateMap<IndexedUserTenant, CurrentUserTenant>()
            .ForMember(t => t.TenantId, m => m.MapFrom(t => t.Id));

        CreateMap<CreateTenant, Tenant>();
        CreateMap<IndexedTenant, TenantSummary>();
        
        CreateMap<CreateAdminUser, User>();
        CreateMap<IndexedUser, UserSummary>();
        
        CreateMap<CreateProject, Project>();
        CreateMap<IndexedProject, ProjectSummary>();
        
        CreateMap<CreateBinary, Binary>()
            .ForMember(b => b.Metadata, m => m.MapFrom(_ => new Dictionary<string, string>()));
    }
}
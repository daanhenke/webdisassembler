using AutoMapper;
using WebDisassembler.Core.Common.Models;
using WebDisassembler.DataStorage.Models.Identity;
using WebDisassemlber.Search.Data.Models;

namespace WebDisassembler.Search.Service.Mapping;

class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap(typeof(PagedResponse<>), typeof(PagedResponse<>));
        CreateMap<User, IndexedUser>();
        CreateMap<Tenant, IndexedTenant>();
    }
}
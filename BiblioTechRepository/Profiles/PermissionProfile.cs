using AutoMapper;
using BiblioTechData.Models;
using BiblioTechDomain.ViewModels.Permissions;

namespace BiblioTechDomain.Profiles
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, ReadPermissionViewModel>();
        }
    }
}

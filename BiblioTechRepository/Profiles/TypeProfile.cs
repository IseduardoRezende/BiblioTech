using AutoMapper;
using BiblioTechDomain.ViewModels.Types;

namespace BiblioTechDomain.Profiles
{
    public class TypeProfile : Profile
    {
        public TypeProfile()
        {
            CreateMap<Type, ReadTypeViewModel>();   
        }
    }
}

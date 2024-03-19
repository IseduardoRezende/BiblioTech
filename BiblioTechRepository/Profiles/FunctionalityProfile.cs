using AutoMapper;
using BiblioTechData.Models;
using BiblioTechDomain.ViewModels.Functionalities;

namespace BiblioTechDomain.Profiles
{
    public class FunctionalityProfile : Profile
    {
        public FunctionalityProfile()
        {
            CreateMap<Functionality, ReadFunctionalityViewModel>();
        }
    }
}

using AutoMapper;
using BiblioTechData.Models;
using BiblioTechDomain.ViewModels.Libraries;

namespace BiblioTechDomain.Profiles
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<CreateLibraryViewModel, Library>();
            CreateMap<UpdateLibraryViewModel, Library>();
            CreateMap<Library, ReadLibraryViewModel>();
        }
    }
}

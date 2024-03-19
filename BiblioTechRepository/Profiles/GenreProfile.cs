using AutoMapper;
using BiblioTechData.Models;
using BiblioTechDomain.ViewModels.Genres;

namespace BiblioTechDomain.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, ReadGenreViewModel>();
        }
    }
}

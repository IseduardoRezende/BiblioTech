using AutoMapper;
using BiblioTechData.Models;
using BiblioTechDomain.ViewModels.Books;

namespace BiblioTechDomain.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookViewModel, Book>();
            CreateMap<UpdateBookViewModel, Book>();
            CreateMap<Book, ReadBookViewModel>();
        }
    }
}

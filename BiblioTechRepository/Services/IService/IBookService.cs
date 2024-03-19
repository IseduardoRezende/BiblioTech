using BiblioTechData.Models;
using BiblioTechDomain.ViewModels.Books;

namespace BiblioTechDomain.Services.IService
{
    public interface IBookService : IBaseService<CreateBookViewModel, UpdateBookViewModel, ReadBookViewModel, Book>
    {
    }
}

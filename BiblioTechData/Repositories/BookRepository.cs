using BiblioTechData.Models;
using BiblioTechData.Repositories.IRepository;

namespace BiblioTechData.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(BiblioTechContext context) : base(context) { }
    }
}

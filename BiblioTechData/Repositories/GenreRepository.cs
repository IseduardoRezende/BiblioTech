using BiblioTechData.Models;
using BiblioTechData.Repositories.IRepository;

namespace BiblioTechData.Repositories
{
    public class GenreRepository : BaseReadOnlyRepository<Genre>, IGenreRepository
    {
        public GenreRepository(BiblioTechContext context) : base(context) { }
    }
}

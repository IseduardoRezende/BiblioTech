using BiblioTechData.Models;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.ViewModels.Genres;

namespace BiblioTech.Controllers
{
    public class GenreController : BaseReadOnlyController<ReadGenreViewModel, Genre>
    {
        public GenreController(IGenreService genreService) : base(genreService) { }
    }
}

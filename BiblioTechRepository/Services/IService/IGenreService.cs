using BiblioTechData.Models;
using BiblioTechDomain.ViewModels.Genres;

namespace BiblioTechDomain.Services.IService
{
    public interface IGenreService : IBaseReadOnlyService<ReadGenreViewModel, Genre>
    {
    }
}

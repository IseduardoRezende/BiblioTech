using AutoMapper;
using BiblioTechData.Models;
using BiblioTechData.Repositories.IRepository;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.Bases;
using BiblioTechDomain.ViewModels.Genres;

namespace BiblioTechDomain.Services
{
    public class GenreService : BaseReadOnlyService<ReadGenreViewModel, Genre>, IGenreService
    {
        public GenreService(IGenreRepository genreRepository, IMapper mapper) 
            : base(genreRepository, mapper) { }

        protected override Func<Genre, bool> Filter(IEnumerable<BaseFilter> filters)
        {
            var description = filters.FirstOrDefault(c => string.Equals(c.Field, "description", StringComparison.OrdinalIgnoreCase));
            var hasDescription = description == null ? false : true;
            var descriptionValue = hasDescription ? description!.Value : string.Empty;

            return a => 
                (!hasDescription || string.IsNullOrEmpty(descriptionValue) || string.Equals(a.Description, descriptionValue, StringComparison.OrdinalIgnoreCase));
        }
    }
}

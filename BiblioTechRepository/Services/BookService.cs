using AutoMapper;
using BiblioTechData.Models;
using BiblioTechData.Repositories.IRepository;
using BiblioTechDomain.Enums;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.Bases;
using BiblioTechDomain.ViewModels.Books;

namespace BiblioTechDomain.Services
{
    public class BookService : BaseService<CreateBookViewModel, UpdateBookViewModel, ReadBookViewModel, Book>, IBookService
    {
        private readonly ILibraryService _libraryService;
        private readonly IGenreService _genreService;

        public BookService(IBookRepository bookRepository, ILibraryService libraryService, IGenreService genreService, IMapper mapper)
            : base(bookRepository, mapper) 
        {
            _libraryService = libraryService;
            _genreService = genreService;
        }

        protected override Func<Book, bool> Filter(IEnumerable<BaseFilter> filters)
        {
            var title = filters.FirstOrDefault(c => string.Equals(c.Field, "title", StringComparison.OrdinalIgnoreCase));
            var hasTitle = title == null ? false : true;
            var titleValue = hasTitle ? title!.Value : string.Empty;

            var initialDate = filters.FirstOrDefault(c => string.Equals(c.Field, "initialDate", StringComparison.OrdinalIgnoreCase));
            var finalDate = filters.FirstOrDefault(c => string.Equals(c.Field, "finalDate", StringComparison.OrdinalIgnoreCase));

            DateTime inDate = DateTime.MinValue;
            DateTime finDate = DateTime.MinValue;

            var hasInitialDate = initialDate == null ? false : DateTime.TryParse(initialDate.Value, out inDate);
            var hasFinalDate = finalDate == null ? false : DateTime.TryParse(finalDate.Value, out finDate);

            var genre = filters.FirstOrDefault(c => string.Equals(c.Field, "genre", StringComparison.OrdinalIgnoreCase));
            var hasGenre = genre == null ? false : true;
            var genreValue = hasGenre ? genre!.Value : string.Empty;

            var status = filters.FirstOrDefault(c => string.Equals(c.Field, "status", StringComparison.OrdinalIgnoreCase));
            int statusType = 0;
            var hasStatus = status == null ? false : int.TryParse(status.Value, out statusType);

            return a =>
            (!hasTitle || string.IsNullOrEmpty(titleValue) || string.Equals(a.Title, titleValue, StringComparison.OrdinalIgnoreCase)) &&
            (!hasInitialDate || !hasFinalDate || (a.ReleaseDate >= inDate && a.ReleaseDate <= finDate)) &&
            (!hasGenre || string.IsNullOrEmpty(genreValue) || a.Genre.Description == genreValue) &&
            (!hasStatus || BaseFilter.ApplyStatusFilter(a, statusType));
        }

        protected override async Task<BaseValidation> IsValidCreate(CreateBookViewModel createModel)
        {
            var validation = new BaseValidation();

            if (createModel == null)
                validation.AddError(new BaseError(nameof(createModel), createModel, Error.Null_Value));

            var library = await _libraryService.FindByAsync(c => c.Id == createModel!.LibraryId);
            
            if (library == null)
                validation.AddError(new BaseError(nameof(createModel.LibraryId), createModel!.LibraryId, Error.Non_Existent_Value));

            var genre = await _genreService.FindByAsync(c => c.Id == createModel!.GenreId);

            if (genre == null)
                validation.AddError(new BaseError(nameof(createModel.GenreId), createModel!.GenreId, Error.Non_Existent_Value));

            if (string.IsNullOrEmpty(createModel!.Title))
                validation.AddError(new BaseError(nameof(createModel.Title), createModel.Title, Error.Invalid_Value));

            if (string.IsNullOrEmpty(createModel.ISBN))
                validation.AddError(new BaseError(nameof(createModel.ISBN), createModel.ISBN, Error.Invalid_Value));
           
            return validation;
        }
        
        protected override async Task<BaseValidation> IsValidUpdate(UpdateBookViewModel updateModel)
        {
            var validation = new BaseValidation();
         
            if (updateModel == null)
                validation.AddError(new BaseError(nameof(updateModel), updateModel, Error.Null_Value));
           
            var genre = await _genreService.FindByAsync(c => c.Id == updateModel!.GenreId);
            
            if (genre == null)
                validation.AddError(new BaseError(nameof(updateModel.GenreId), updateModel!.GenreId, Error.Non_Existent_Value));

            if (string.IsNullOrEmpty(updateModel!.Title))
                validation.AddError(new BaseError(nameof(updateModel.Title), updateModel.Title, Error.Invalid_Value));

            if (string.IsNullOrEmpty(updateModel.ISBN))
                validation.AddError(new BaseError(nameof(updateModel.ISBN), updateModel.ISBN, Error.Invalid_Value));
            
            return validation;
        }

        protected override Book UpdateProperties(Book model, UpdateBookViewModel updateModel)
        {
            model.Title = updateModel.Title;
            model.ISBN = updateModel.ISBN;
            model.ReleaseDate = updateModel.ReleaseDate;
            model.Volume = updateModel.Volume;
            model.Exemplary = updateModel.Exemplary;
            model.GenreId = updateModel.GenreId;
            model.Pages = updateModel.Pages;

            return model;
        }
    }
}

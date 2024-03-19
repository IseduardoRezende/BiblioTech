using BiblioTechData.Models;
using BiblioTechDomain.ViewModels.Libraries;

namespace BiblioTechDomain.Services.IService
{
    public interface ILibraryService : IBaseService<CreateLibraryViewModel, UpdateLibraryViewModel, ReadLibraryViewModel, Library>
    {

    }
}

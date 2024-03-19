using BiblioTechData.Models;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.ViewModels.Functionalities;

namespace BiblioTech.Controllers
{
    public class FunctionalityController : BaseReadOnlyController<ReadFunctionalityViewModel, Functionality>
    {
        public FunctionalityController(IFunctionalityService functionalityService) : base(functionalityService)
        {
        }
    }
}

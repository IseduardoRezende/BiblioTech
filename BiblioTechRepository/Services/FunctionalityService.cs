using AutoMapper;
using BiblioTechData.Models;
using BiblioTechData.Repositories.IRepository;
using BiblioTechDomain.Bases;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.ViewModels.Functionalities;

namespace BiblioTechDomain.Services
{
    public class FunctionalityService : BaseReadOnlyService<ReadFunctionalityViewModel, Functionality>, IFunctionalityService
    {
        public FunctionalityService(IFunctionalityRepository functionalityRepository, IMapper mapper) 
            : base(functionalityRepository, mapper) { }

        protected override Func<Functionality, bool> Filter(IEnumerable<BaseFilter> filters)
        {
            return a => true;
        }
    }
}

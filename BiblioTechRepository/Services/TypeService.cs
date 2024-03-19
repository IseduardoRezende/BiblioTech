using AutoMapper;
using BiblioTechData.Repositories.IRepository;
using BiblioTechDomain.Bases;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.ViewModels.Types;

namespace BiblioTechDomain.Services
{
    public class TypeService : BaseReadOnlyService<ReadTypeViewModel, Type>, ITypeService
    {
        public TypeService(ITypeRepository typeRepository, IMapper mapper) 
            : base(typeRepository, mapper) { }

        protected override Func<Type, bool> Filter(IEnumerable<BaseFilter> filters)
        {
            return a => true;
        }
    }
}

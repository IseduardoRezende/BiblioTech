global using Type = BiblioTechData.Models.Type;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.ViewModels.Types;

namespace BiblioTech.Controllers
{
    public class TypeController : BaseReadOnlyController<ReadTypeViewModel, Type>
    {
        public TypeController(ITypeService typeService) : base(typeService)
        {
        }
    }
}

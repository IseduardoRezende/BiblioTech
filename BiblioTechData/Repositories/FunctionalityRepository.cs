using BiblioTechData.Models;
using BiblioTechData.Repositories.IRepository;

namespace BiblioTechData.Repositories
{
    public class FunctionalityRepository : BaseReadOnlyRepository<Functionality>, IFunctionalityRepository
    {
        public FunctionalityRepository(BiblioTechContext context) : base(context) {  }
    }
}

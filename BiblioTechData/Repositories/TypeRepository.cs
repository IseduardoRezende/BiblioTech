using BiblioTechData.Repositories.IRepository;

namespace BiblioTechData.Repositories
{
    public class TypeRepository : BaseReadOnlyRepository<Type>, ITypeRepository
    {
        public TypeRepository(BiblioTechContext context) : base(context) { }
    }
}

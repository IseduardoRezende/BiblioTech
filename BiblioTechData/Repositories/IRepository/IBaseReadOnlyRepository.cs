using BiblioTechData.Enums;
using BiblioTechData.Interfaces;

namespace BiblioTechData.Repositories.IRepository
{
    public interface IBaseReadOnlyRepository<Model> where Model : IBaseModel
    {
        Task<Model?> FindByAsync(Func<Model, bool> predicate, params string[] includes);

        Task<IEnumerable<Model>> ListAsync(
            Func<Model, bool> filter,
            string orderField,
            OrderType orderType,
            int offSet,
            short itemsPerPage,
            params string[] includes);
        
        Task<long> CountAsync(Func<Model, bool> filter);
    }
}

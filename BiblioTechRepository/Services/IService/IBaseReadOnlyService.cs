using BiblioTechData.Interfaces;
using BiblioTechDomain.Interfaces;
using BiblioTechDomain.Bases;
using BiblioTechData.Enums;

namespace BiblioTechDomain.Services.IService
{
    public interface IBaseReadOnlyService<ReadModel, Model>
        where ReadModel : IReadModel
        where Model : IBaseModel
    {
        Task<ReadModel?> FindByAsync(Func<Model, bool> predicate, params string[] includes);

        Task<BaseList<ReadModel>> ListAsync(
            IEnumerable<BaseFilter> filters, 
            string orderField,
            OrderType orderType,
            int offSet,
            short itemsPerPage,
            params string[] includes);

        Task<long> CountAsync(Func<Model, bool> filter);
    }
}

using BiblioTechData.Interfaces;

namespace BiblioTechData.Repositories.IRepository
{
    public interface IBaseRepository<Model> : IBaseReadOnlyRepository<Model> 
        where Model : IBaseModel
    {
        Task<Model> CreateAsync(Model model);

        Task<Model> UpdateAsync(Model model);

        Task<bool> DeleteAsync(Model model);

        Task<bool> DeletePermanentAsync(Model model);

        Task<bool> ActiveAsync(Model model);
    }
}

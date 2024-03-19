using BiblioTechData.Interfaces;
using BiblioTechDomain.Interfaces;

namespace BiblioTechDomain.Services.IService
{
    public interface IBaseService<CreateModel, UpdateModel, ReadModel, Model> : IBaseReadOnlyService<ReadModel, Model>
        where CreateModel : ICreateModel
        where UpdateModel : IUpdateModel
        where ReadModel : IReadModel
        where Model : IBaseModel
    {
        Task<ReadModel> CreateAsync(CreateModel createModel);

        Task<ReadModel> UpdateAsync(UpdateModel updateModel);

        Task<bool> DeleteAsync(long Id);

        Task<bool> DeletePermanentAsync(long Id);
    
        Task<bool> ActiveAsync(long Id);
    }
}

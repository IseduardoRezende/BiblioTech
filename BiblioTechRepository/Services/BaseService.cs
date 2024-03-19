using AutoMapper;
using BiblioTechData.Interfaces;
using BiblioTechData.Repositories.IRepository;
using BiblioTechDomain.Bases;
using BiblioTechDomain.Enums;
using BiblioTechDomain.Interfaces;
using BiblioTechDomain.Services.IService;

namespace BiblioTechDomain.Services
{
    public abstract class BaseService<CreateModel, UpdateModel, ReadModel, Model> : BaseReadOnlyService<ReadModel, Model>, IBaseService<CreateModel, UpdateModel, ReadModel, Model>
        where CreateModel : ICreateModel
        where UpdateModel : IUpdateModel
        where ReadModel : IReadModel, new()
        where Model : class, IBaseModel
    {
        protected IBaseRepository<Model> _repository;

        protected BaseService(IBaseRepository<Model> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<ReadModel> CreateAsync(CreateModel createModel)
        {
            var validation = await IsValidCreate(createModel);

            if (validation.HasErros && validation.Errors.Any())
                return base.BuildReadModel(validation);

            var model = _mapper.Map<Model>(createModel);
            var createdModel = await _repository.CreateAsync(model);
            return _mapper.Map<ReadModel>(createdModel);
        }

        protected abstract Task<BaseValidation> IsValidCreate(CreateModel createModel);

        public virtual async Task<ReadModel> UpdateAsync(UpdateModel updateModel)
        {
            var validation = await IsValidUpdate(updateModel);

            if (validation.HasErros && validation.Errors.Any())
                return base.BuildReadModel(validation);

            var model = await _readOnlyRepository.FindByAsync(c => c.Id == updateModel.Id);

            if (model == null)
                return base.BuildReadModel(validation, new BaseError(nameof(updateModel.Id), updateModel.Id, Error.Non_Existent_Value));            

            model = UpdateProperties(model, updateModel);

            var updatedModel = await _repository.UpdateAsync(model);
            return _mapper.Map<ReadModel>(updatedModel);
        }

        protected abstract Model UpdateProperties(Model model, UpdateModel updateModel);

        protected abstract Task<BaseValidation> IsValidUpdate(UpdateModel updateModel);

        public virtual async Task<bool> DeleteAsync(long Id)
        {
            var model = await _readOnlyRepository.FindByAsync(c => c.Id == Id);

            if (model == null || model.DeletedAt != null)
                return false;

            return await _repository.DeleteAsync(model);
        }

        public virtual async Task<bool> DeletePermanentAsync(long Id)
        {
            var model = await _readOnlyRepository.FindByAsync(c => c.Id == Id);

            if (model == null)
                return false;

            return await _repository.DeletePermanentAsync(model);
        }

        public virtual async Task<bool> ActiveAsync(long Id)
        {
            var model = await _readOnlyRepository.FindByAsync(c => c.Id == Id);

            if (model == null || model.DeletedAt == null)
                return false;

            return await _repository.ActiveAsync(model);
        }
    }
}

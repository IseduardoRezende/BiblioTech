using AutoMapper;
using BiblioTechData.Interfaces;
using BiblioTechData.Repositories.IRepository;
using BiblioTechDomain.Interfaces;
using BiblioTechDomain.Services.IService;
using BiblioTechDomain.Bases;
using BiblioTechData.Enums;

namespace BiblioTechDomain.Services
{
    public abstract class BaseReadOnlyService<ReadModel, Model> : IBaseReadOnlyService<ReadModel, Model>
        where ReadModel : IReadModel, new()
        where Model : class, IBaseModel
    {
        protected IBaseReadOnlyRepository<Model> _readOnlyRepository;
        protected IMapper _mapper;

        protected BaseReadOnlyService(IBaseReadOnlyRepository<Model> readOnlyRepository, IMapper mapper)
        {
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
        }

        public virtual async Task<BaseList<ReadModel>> ListAsync(
            IEnumerable<BaseFilter> filters,
            string orderField,
            OrderType orderType,
            int offSet,
            short itemsPerPage,
            params string[] includes)
        {
            var count = await CountAsync(Filter(filters));

            var items = await _readOnlyRepository.ListAsync(
                Filter(filters),
                orderField,
                orderType,
                offSet,
                itemsPerPage,
                includes);

            var currentPage = (offSet / itemsPerPage) + 1;
            var baseList = new BaseList<ReadModel>(currentPage, itemsPerPage, count);

            foreach (var item in items)
            {
                var mapItem = _mapper.Map<ReadModel>(item);
                baseList.AddItem(mapItem);
            }

            return baseList;
        }

        public virtual async Task<long> CountAsync(Func<Model, bool> filter)
        {
            return await _readOnlyRepository.CountAsync(filter);
        }

        public virtual async Task<ReadModel?> FindByAsync(Func<Model, bool> predicate, params string[] includes)
        {
            var entity = await _readOnlyRepository.FindByAsync(predicate, includes);
            return entity == null ? default : _mapper.Map<ReadModel>(entity);
        }

        protected abstract Func<Model, bool> Filter(IEnumerable<BaseFilter> filters);

        protected ReadModel BuildReadModel(BaseValidation validation)
        {
            return new ReadModel() { Validation = validation };
        }

        protected ReadModel BuildReadModel(BaseValidation validation, BaseError error)
        {
            validation.AddError(error);
            return new ReadModel() { Validation = validation };
        }
        
        protected ReadModel BuildReadModel(BaseError error)
        {
            var validation = new BaseValidation();
            validation.AddError(error);
            return new ReadModel() { Validation = validation };
        }
    }
}

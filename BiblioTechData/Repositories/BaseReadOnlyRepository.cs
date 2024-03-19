using BiblioTechData.Enums;
using BiblioTechData.Extensions;
using BiblioTechData.Interfaces;
using BiblioTechData.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BiblioTechData.Repositories
{
    public abstract class BaseReadOnlyRepository<Model> : IBaseReadOnlyRepository<Model>
        where Model : class, IBaseModel
    {
        protected readonly BiblioTechContext _context;

        protected BaseReadOnlyRepository(BiblioTechContext context)
        {
            _context = context;
        }

        protected DbSet<Model> Entity
        {
            get
            {
                return _context.Set<Model>();
            }
        }
        
        public virtual async Task<IEnumerable<Model>> ListAsync(
            Func<Model, bool> filter,
            string orderField,
            OrderType orderType,
            int offSet,
            short itemsPerPage,
            params string[] includes)
        {
            IQueryable<Model> query = Entity;

            foreach (var include in includes)            
                query = query.Include(include);            

            return await Task.FromResult(
                query.Where(filter)
                .OrderList(orderField, orderType)
                .Skip(offSet > 1 ? (offSet - 1) * int.Parse(itemsPerPage.ToString()) : 0)
                .Take(itemsPerPage)
                .ToList());
        }
        
        public Task<long> CountAsync(Func<Model, bool> filter)
        {
            IQueryable<Model> query = Entity;
            return Task.FromResult(query.Where(filter).LongCount());
        }

        public virtual async Task<Model?> FindByAsync(Func<Model, bool> predicate, params string[] includes)
        {
            IQueryable<Model> query = Entity;

            foreach (var include in includes)            
                query = query.Include(include);

            return await Task.FromResult(query.FirstOrDefault(predicate));
        }
    }
}

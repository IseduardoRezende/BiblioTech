using BiblioTechData.Interfaces;
using BiblioTechData.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BiblioTechData.Repositories
{
    public abstract class BaseRepository<Model> : BaseReadOnlyRepository<Model>, IBaseRepository<Model>
        where Model : class, IBaseModel
    {
        protected BaseRepository(BiblioTechContext context) : base(context) { }                        

        public virtual async Task<Model> CreateAsync(Model model)
        {
            var attach = _context.Entry(model);
            attach.State = EntityState.Added;
            await SaveChangesAsync();
            return await base.FindByAsync(c => c.Id == attach.Entity.Id);
        }

        public virtual async Task<Model> UpdateAsync(Model model)
        {
            var attach = _context.Entry(model);
            attach.State = EntityState.Modified;
            await SaveChangesAsync();
            return await base.FindByAsync(c => c.Id == attach.Entity.Id);
        }

        public virtual async Task<bool> DeleteAsync(Model model)
        {
            model.DeletedAt = DateTime.Now;
            var attach = _context.Entry(model);
            attach.State = EntityState.Modified;
            return await SaveChangesAsync();
        }

        public virtual async Task<bool> DeletePermanentAsync(Model model)
        {            
            var attach = _context.Entry(model);
            attach.State = EntityState.Deleted;
            return await SaveChangesAsync();
        }

        public virtual async Task<bool> ActiveAsync(Model model)
        {
            model.DeletedAt = null;
            var attach = _context.Entry(model);
            attach.State = EntityState.Modified;
            return await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }        
    }
}

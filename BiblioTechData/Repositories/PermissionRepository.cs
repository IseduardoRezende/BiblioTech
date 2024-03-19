using BiblioTechData.Models;
using BiblioTechData.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BiblioTechData.Repositories
{
    public class PermissionRepository : BaseReadOnlyRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(BiblioTechContext context) : base(context) { }

        public async Task<IEnumerable<Permission>> GetPermissionsAsync(long typeId)
        {
            return await Entity
                .Include(c => c.Functionality)
                .Where(c => c.TypeId == typeId)
                .ToListAsync();
        }
    }
}

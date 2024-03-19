using BiblioTechData.Models;

namespace BiblioTechData.Repositories.IRepository
{
    public interface IPermissionRepository : IBaseReadOnlyRepository<Permission>
    {
        Task<IEnumerable<Permission>> GetPermissionsAsync(long typeId);
    }
}

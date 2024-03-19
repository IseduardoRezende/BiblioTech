using BiblioTechData.Models;
using BiblioTechDomain.ViewModels.Permissions;

namespace BiblioTechDomain.Services.IService
{
    public interface IPermissionService : IBaseReadOnlyService<ReadPermissionViewModel, Permission>
    {
        Task<IEnumerable<ReadPermissionViewModel>> GetPermissionsAsync(long typeId);
    }
}
